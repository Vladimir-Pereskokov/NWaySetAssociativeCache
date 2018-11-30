/*Date: 12/1/2017
 * Author: Vladimir Pereskokov
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSetACache.Interfaces;

namespace NSetACache
{
    /// <summary>
    /// Defines caching algorithm types.
    /// </summary>
    public enum CachingAlgorithm : byte
    {
        FullyAssociative = 0, DirectMapped = 1, SetAssociative = 2
    }

    /// <summary>
    /// Generic cache type that supports three types of cache: N-way set associative cache, Direct mapped and Fully associative cache.
    /// </summary>
    /// <typeparam name="TValue">Type of value to be stored in cache</typeparam>
    /// <typeparam name="TKey">Type of key to be used to query the cache for values</typeparam>
    public class Cache<TValue, TKey> :IDisposable
    {
        internal const short MinItemsPerSetForMultithreadScan = short.MaxValue / 64; //512 items to trigger the threads min
        private const uint NegativeIntShift = (uint)int.MaxValue + 1;

        private CacheItem<TValue, TKey>[] m_cachePlus = null;
        private CacheItem<TValue, TKey>[] m_cacheMinus = null;

        private uint m_SetAddressMask = 0;
        // Defines a delegate that is used to calculate the hash code for a given <see cref="TKey"/> value.
        private Func<TKey, int> m_HashCalc;
        // Defines a delegate used to load a value from main storage given a key value.
        private Func<TKey, TValue> m_LoadHandler;
        // Defines a delegate that is used to create instances of<see cref="CacheItem<TValue, TKey>"/>
        private Func<TValue, TKey, CacheItem<TValue, TKey>> m_itemCreateHr;
        private Action<ReplacementArgs<TValue, TKey>> m_replacementHandler;        

        private readonly byte m_MaxScanThreads;
        private readonly Config<TValue, TKey> Config;

        /// <summary>
        /// Constructs new cache object for a given number of <see cref="sizeBits"/>.
        /// </summary>
        /// <param name="sizeBits">number of bits to be used for addressing cache items</param>
        /// <remarks>Cache Size = 2 to the power of <see cref="sizeBits"/></remarks>
        public Cache(byte sizeBits): this(new Config<TValue, TKey>(sizeBits)) {}

        /// <summary>
        /// Constructs new cache object for a given <see cref="Config{TValue, TKey}"/>.
        /// </summary>
        /// <param name="config">Cache configuration obect to be used for cache construction purposes.</param>
        public Cache(Config<TValue, TKey> config) : this(config, null) { }

        /// <summary>
        /// Constructs new cache object for a given <see cref="Config{TValue, TKey}"/> and initialLoader object.
        /// </summary>
        /// <param name="config">Cache configuration obect to be used for cache construction purposes.</param>
        /// <param name="initialLoader">Initial loader implementation to be used for cache initialization</param>
        public Cache(Config<TValue, TKey> config, ICacheLoadProvider<TValue, TKey> initialLoader) : base()
        {
            if (config == null)
            {
                throw new ArgumentException("config argument is reguired.");
            }
            this.Config = config;
            if (config.Strategy == ReplacementStrategy.Custom && (config.ReplacementHandler != null))
            {
                m_replacementHandler = config.ReplacementHandler;
            }                
            m_cachePlus = new CacheItem<TValue, TKey>[config.Size - 1];
            m_cacheMinus = new CacheItem<TValue, TKey>[config.Size - 1];
            if (config.CacheType != CachingAlgorithm.FullyAssociative) m_SetAddressMask = (uint)(2 << config.SetSizeBits) - 1;
            m_HashCalc = config.HashCalculator ?? DefaultCalcHashFunc;            
            m_itemCreateHr = config.CreateItemHandler ?? DefaultItemCreateHr;
            m_LoadHandler = config.LoadHandler;
            m_MaxScanThreads = (config.ThreadsMaxNumberOfScan > 0)? config.ThreadsMaxNumberOfScan : Config<TValue, TKey>.DefaultMaxNumberOfScanThreads;                      
            LoadData(initialLoader, config);
        }
        
        /// <summary>
        /// Loads initial cache data
        /// </summary>
        /// <param name="initialLoader"></param>
        /// <param name="cfg"></param>
        private void LoadData(ICacheLoadProvider<TValue, TKey> initialLoader, Config<TValue, TKey> cfg) {
            if (initialLoader != null) {
                var intLoadTimeMaxSecs = cfg.LoadCacheMaxSeconds;
                var numberOfSets = this.Config.NumberOfSets;
                var itemsPerSet = this.Config.ItemsPerSet;
                var arrSetIDXs = new int[this.Config.NumberOfSets];
                arrSetIDXs[0] = -1;
                int totalNumberLoaded = 0;
                int totalNumberOfFullSets = 0;
                DateTime start = DateTime.UtcNow;
                while (initialLoader.GetNextKey(totalNumberLoaded, cfg,
                     (iSet) => {
                         return (iSet > -1 && iSet < numberOfSets) ? arrSetIDXs[iSet] : -1;
                     },
                    out TKey key))
                {
                    int setNumber = FindSetNumber(key, out CacheItem<TValue, TKey>[] d);
                    if (setNumber > -1) {
                        var idxMax = arrSetIDXs[setNumber];
                        if ((idxMax == 0 && setNumber > 0) || 
                            (idxMax ==-1 && setNumber ==0) || 
                            (idxMax > 0 && (idxMax < itemsPerSet - 1))) { //Check if this set is not full
                            TValue val = initialLoader.GetValue(key);
                            if (idxMax < 0) idxMax = 0;
                            d[idxMax + this.SetNumberToSetAddress(setNumber)] = m_itemCreateHr(val, key);                                                        
                            arrSetIDXs[setNumber] = idxMax + 1;
                            totalNumberLoaded += 1;
                            if (idxMax >= (itemsPerSet - 1)) totalNumberOfFullSets += 1; 
                        }
                        if ((totalNumberOfFullSets >= numberOfSets) || 
                            (intLoadTimeMaxSecs > 0 && (DateTime.UtcNow - start).TotalSeconds >= intLoadTimeMaxSecs))
                            break; //finish when all sets are full or the time elapsed is greater than a limit set by Config
                    }
                }

            }
        }

        private CacheItem<TValue, TKey> DefaultItemCreateHr( TValue value, TKey key) => new CacheItem<TValue, TKey>(value, key);

        private int DefaultCalcHashFunc(TKey key) => (key != null) ? key.GetHashCode() : 0; 

        private int FindSetNumber(TKey key, out CacheItem<TValue, TKey>[] cacheData) => FindSetNumber(m_HashCalc(key), out cacheData);
        

        private int FindSetNumber(int key, out CacheItem<TValue, TKey>[] cacheData) {
            int effectiveKey = (key >= 0) ? key : (int)((uint)NegativeIntShift + key);
            var data = (key < 0) ? m_cacheMinus: m_cachePlus;
            cacheData = data;

            switch (Config.CacheType) {
                case CachingAlgorithm.SetAssociative: return (int)((uint)effectiveKey & m_SetAddressMask); //N way Set Associative cache
                case CachingAlgorithm.DirectMapped: return effectiveKey; //Direct mapped cache. Set start address is actual address in array
                default: return 0; //this is fully associative cache. We start at the top every time. 
            }              
        }

        private int SetNumberToSetAddress(int setNumber) => setNumber * Config.ItemsPerSet;


        /// <summary>
        /// Scan result structure used for internal operations
        /// </summary>
        protected internal struct ScanResult : IDisposable {
            public ScanResult(int firstEmpty, CacheItem<TValue, TKey>[] d) {
                FirstEmptySlotIDX = firstEmpty;
                FoundItemIDX = -1;
                EvictionCandidateIDX = -1;
                data = d;
            }

            internal ScanResult(bool empty) {
                FirstEmptySlotIDX = -1;
                FoundItemIDX = -1;
                EvictionCandidateIDX = -1;
                data = null;
            }

            public bool IsEmpty => ((FirstEmptySlotIDX < 0 && FoundItemIDX < 0 && EvictionCandidateIDX <0) || data == null);

            internal int FoundItemIDX;
            internal int EvictionCandidateIDX;
            internal int FirstEmptySlotIDX;
            internal CacheItem<TValue, TKey>[] data;

            internal void OccupyFirstEmptySlot(CacheItem<TValue, TKey> item) {
                if (FirstEmptySlotIDX > -1 && item != null) {
                    data[FirstEmptySlotIDX] = item;
                    FirstEmptySlotIDX = -1;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj != null && obj is ScanResult) {
                    return Equals((ScanResult)obj);                    
                }
                return false;
            }

            public override int GetHashCode()
            {
                return (int)(((long)FirstEmptySlotIDX + (long)EvictionCandidateIDX + (long)FoundItemIDX) % int.MaxValue);                
            }

            public bool Equals(ScanResult other) {
                return other.FoundItemIDX == FoundItemIDX && other.FirstEmptySlotIDX == FirstEmptySlotIDX
                                && other.EvictionCandidateIDX == EvictionCandidateIDX && other.data == data;
            }

            public void Dispose()
            {
                if (data != null) {
                    if (FirstEmptySlotIDX > -1) {
                        var emptyItm = data[FirstEmptySlotIDX];
                        if (emptyItm != null && emptyItm.State == CacheItemState.PlaceHolder ) data[FirstEmptySlotIDX] = null;
                    }
                    data = null;
                }                
            }
        }


        /// <summary>
        /// Scans the cache for a given key
        /// </summary>
        /// <param name="key">Key value to use to pull corresponding value from cache</param>      
        /// <returns></returns>
        protected ScanResult ScanForItem(TKey key) {            
            int startIdx = FindSetNumber(key, out CacheItem<TValue, TKey>[] d);
            if (startIdx >-1 && d != null) {
                var ItemsPerSet = Config.ItemsPerSet;
                var MinItemsPerSetToTriggerThreading = Config.ThreadsMinNumberItemsPerSlot;
                if (ItemsPerSet < MinItemsPerSetToTriggerThreading || m_MaxScanThreads < 2) {
                    return ScanForItemInRange(key, d, startIdx, startIdx + ItemsPerSet -1);                                       
                }
                else
                {
                    var TaskChunk = ItemsPerSet / m_MaxScanThreads;
                    int taskStartIDX = startIdx;
                    var tasks = new Task<ScanResult>[m_MaxScanThreads];
                    try
                    {
                        for (int idx = 0; idx < m_MaxScanThreads; idx++)
                        {
                            if (idx == m_MaxScanThreads - 1)
                            {
                                tasks[idx] = Task<ScanResult>.Run(() => ScanForItemInRange(key, d, taskStartIDX, startIdx + ItemsPerSet - taskStartIDX - 1));
                            }
                            else
                            {
                                tasks[idx] = Task<ScanResult>.Run(() => ScanForItemInRange(key, d, taskStartIDX, taskStartIDX + TaskChunk - 1));
                                taskStartIDX += TaskChunk;
                            }
                        }
                        Task.WaitAll(tasks);
                        var resultValue = new ScanResult(true);
                        var emptySlotValue = new ScanResult(true);
                        var evictValue = new ScanResult(true);
                        var lstResults = new List<ScanResult>();
                        foreach (var t in tasks)
                        {
                            var aw = t.GetAwaiter();
                            var result = aw.GetResult();
                            lstResults.Add(result);
                            if (result.FoundItemIDX > -1) resultValue = result;
                            else if (result.FirstEmptySlotIDX > -1) emptySlotValue = result;
                            else  if (evictValue.IsEmpty)  evictValue = result;
                        }
                        if (resultValue.IsEmpty) {
                            resultValue = emptySlotValue.IsEmpty ? evictValue : emptySlotValue;                            
                        }
                        foreach (var result in lstResults) if (!result.Equals(resultValue)) result.Dispose();                        
                        tasks = null;
                        return resultValue;
                    }
                    catch
                    {
                        throw;
                    }
                    finally {
                        if (tasks != null) {
                            foreach (var t in tasks) {
                                if (t != null) {
                                    var aw = t.GetAwaiter();
                                    if (!aw.IsCompleted) t.Wait();                                    
                                    var result = aw.GetResult();
                                    if (result.FirstEmptySlotIDX > -1) result.Dispose();
                                }
                            }
                        }
                    }

                    
                }
            }            
            return default(ScanResult);
        }

        /// <summary>
        /// Scans for cache iten given a range of cache addresses
        /// </summary>
        /// <param name="key">TKey value</param>
        /// <param name="d">data array instance to scan</param>
        /// <param name="startIDX">start address</param>
        /// <param name="endIDX">end address</param>
        /// <returns>ScanResult structure</returns>
        private ScanResult ScanForItemInRange(TKey key ,
                                  CacheItem<TValue, TKey>[] d, int startIDX, int endIDX) {            
            var evictStrategy = GetCurrentEvictionStrategy(startIDX, d, out ReplacementArgs<TValue, TKey> args);
            int evictCandIDX = startIDX;
            CacheItem<TValue, TKey> evictCand = null;
            bool bFoundFinalEvictionCandidate = false;
            int FirstEmptyIdx = -1;

            for (int idx = startIDX; idx < endIDX; idx++)
            {
                var itm = d[idx];
                if (itm == null) {if (FirstEmptyIdx < 0) FirstEmptyIdx = idx; }
                else
                {
                    if (itm.KeyEqualsTo(key))
                    { //found match
                        return new ScanResult(FirstEmptyIdx, d) {FoundItemIDX = idx};
                    }
                    //FoundAtIndex = idx; return itm;}
                    else
                    {
                        if (!bFoundFinalEvictionCandidate)
                        {
                            if (args != null) args.CurrentIndex = idx;
                            if (evictStrategy(evictCand, itm, args, ref bFoundFinalEvictionCandidate))
                            {
                                if (args == null || args.CommitItemWithIndexForEvicton < 0)
                                {
                                    evictCandIDX = idx;
                                    evictCand = itm;
                                }
                                else
                                {
                                    evictCandIDX = args.CommitItemWithIndexForEvicton;
                                    evictCand = args.Fetcher(args.CommitItemWithIndexForEvicton);
                                }
                            }
                        }
                    }
                }
            }            
            return new ScanResult(FirstEmptyIdx, d){EvictionCandidateIDX = evictCandIDX};
        }


        internal delegate bool EvictionStrategyHandler(CacheItem<TValue, TKey> currentCandidate,
            CacheItem<TValue, TKey> testItem, ReplacementArgs<TValue, TKey> args, ref bool candidateFound);


        /// <summary>
        /// LRU eviction strategy handler
        /// </summary>
        /// <param name="currentCandidate"></param>
        /// <param name="testItem"></param>
        /// <param name="args"></param>
        /// <param name="candidateFound"></param>
        /// <returns></returns>
        private bool EvictionStrategyLRU(CacheItem<TValue, TKey> currentCandidate, 
            CacheItem<TValue, TKey> testItem, ReplacementArgs<TValue, TKey> args, ref bool candidateFound) => ((currentCandidate == null) || currentCandidate.TimeStamp > testItem.TimeStamp);

        /// <summary>
        /// MRU eviction strategy handler
        /// </summary>
        /// <param name="currentCandidate"></param>
        /// <param name="testItem"></param>
        /// <param name="args"></param>
        /// <param name="candidateFound"></param>
        /// <returns></returns>
        private bool EvictionStrategyMRU(CacheItem<TValue, TKey> currentCandidate,
            CacheItem<TValue, TKey> testItem, ReplacementArgs<TValue, TKey> args, ref bool candidateFound) => ((currentCandidate == null) || currentCandidate.TimeStamp < testItem.TimeStamp);


        private bool EvictionStrategyCustom(CacheItem<TValue, TKey> currentCandidate,
            CacheItem<TValue, TKey> testItem, ReplacementArgs<TValue, TKey> args, ref bool candidateFound) {            
            
            if (m_replacementHandler != null && args != null && (args.CommitItemWithIndexForEvicton < 0))
            {
                m_replacementHandler(args);
                int committedITDX = args.CommitItemWithIndexForEvicton;
                if (committedITDX > -1 && committedITDX >= args.StartIndex && committedITDX <= args.EndIndex)
                {
                    candidateFound = true;
                    return true;
                }
                else args.CommitItemWithIndexForEvicton = -1; // reset in case custom provider supplied invalid index value
            }
            return false;
        }

        private EvictionStrategyHandler GetCurrentEvictionStrategy(int startIDX,
                                                    CacheItem<TValue, TKey>[] d, out ReplacementArgs<TValue, TKey> args) {
            args = null;
            switch (Config.Strategy) {
                case ReplacementStrategy.LRU: return EvictionStrategyLRU;
                case ReplacementStrategy.MRU: return EvictionStrategyMRU;
                default: {
                        args = new ReplacementArgs<TValue, TKey>(startIDX, startIDX + Config.ItemsPerSet - 1, idx => { return d[idx]; });
                        return EvictionStrategyCustom;
                }
            }            
        }
               

        /// <summary>
        /// Pushes value item into the cache
        /// </summary>
        /// <param name="value">Value to push in</param>
        /// <param name="key">Key to store with the value</param>
        /// <returns><see cref="Cache{TValue, TKey}"/> object that holds reference to both Value and Key</returns>
        public CacheItem<TValue, TKey> PushItem(TValue value, TKey key) {
            if (m_cachePlus == null) return null;
            try
            {
                using (var resScan = ScanForItem(key)) {
                    var result = m_itemCreateHr(value, key);
                    if (resScan.FoundItemIDX > -1) resScan.data[resScan.FoundItemIDX] = result;   // item exists. simply replace it with new one
                    else if (resScan.FirstEmptySlotIDX > -1) resScan.OccupyFirstEmptySlot(result); // item does not exist but there is at least one empty slot. Use it.
                    else resScan.data[resScan.EvictionCandidateIDX] = result; // Item does not exist and there is no empty slots. Must evict
                    return result;
                }
            }
            catch {
                throw; }            
            
        }      


        /// <summary>
        /// Pulls item from cache.
        /// </summary>
        /// <param name="key">Key value to be used to scan for the value item</param>
        /// <param name="value">Used to return value item in case it was founded in cache or pulled in from the main store.</param>
        /// <returns>True in case value was found.</returns>
        public bool PullItem(TKey key, out TValue value) {

            if (m_cachePlus == null) { value = default(TValue); return false; }
            try
            {
                using (var resScan = ScanForItem(key))
                {
                    CacheItem<TValue, TKey> itm = null;
                    if (resScan.FoundItemIDX > -1)
                    { //found existing item
                        itm = resScan.data[resScan.FoundItemIDX];
                        if (itm != null)
                        {
                            //check for expiration
                            if (Config.CacheExpirationTicks > 0)
                            {
                                var dat = DateTime.UtcNow;
                                long tick = dat.Ticks;
                                if ((tick - Config.CacheExpirationTicks) > itm.TimeStamp)
                                {
                                    itm = null; //invalidate
                                    resScan.data[resScan.FoundItemIDX] = null;
                                }
                                else itm.TimeStamp = tick; //update the timestamp
                            }
                            else itm.TimeStamp = DateTime.UtcNow.Ticks; //update the timestamp
                        }                        
                    }

                    if (m_LoadHandler != null && itm == null)
                    {
                        var val = m_LoadHandler(key);
                        itm = m_itemCreateHr(val, key);
                        if (resScan.FirstEmptySlotIDX > -1) resScan.OccupyFirstEmptySlot(itm);
                        else if (resScan.EvictionCandidateIDX > -1) resScan.data[resScan.EvictionCandidateIDX] = itm;
                    }
                    var Result = (itm != null);
                    value = Result ? itm.Value : default(TValue);
                    return Result;
                }
            }
            catch {
                throw; }
        }

        /// <summary>
        /// Removes value item from the cache for a given Key
        /// </summary>
        /// <param name="key">Key value to remove the Value item for</param>
        public void ClearItem(TKey key) {
            if (m_cachePlus == null) return;
            using (var res = ScanForItem(key)) {
                if (res.FoundItemIDX > -1) { res.data[res.FoundItemIDX] = null; }
            }                
        }

        public void Dispose() {
            m_cacheMinus = null;
            m_cachePlus = null;
            m_HashCalc = null;
            m_itemCreateHr = null;
            m_LoadHandler = null;
            m_replacementHandler = null;
        }
    }
}
