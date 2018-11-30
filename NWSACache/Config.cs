using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSetACache
{
    /// <summary>
    /// Defines three cache item eviction strategy types : MRU, LRU, Custom
    /// </summary>
    public enum ReplacementStrategy : byte { LRU = 0, MRU = 1, Custom =2}

    /// <summary>
    /// Describes different statuses of chache scans.
    /// </summary>
    public enum ReplacementScanStatus : byte { Scanning = 0, EmptySlotFound = 1, MatchFound = 2}       

    public class ReplacementArgs<TValue, TKey> : EventArgs {
        private Func<int, CacheItem<TValue, TKey>> m_rawFetcher;
        internal ReplacementArgs(int startIdx, int endIdx, Func<int, CacheItem<TValue, TKey>> rawFetcher): base() {
            StartIndex = startIdx;
            CurrentIndex = StartIndex;
            EndIndex = endIdx;
            m_rawFetcher = rawFetcher;
            Fetcher = FetcherInternal;
        }

        private CacheItem<TValue, TKey> FetcherInternal(int idx) {
            if (idx < StartIndex || idx > EndIndex) throw new ArgumentOutOfRangeException($"index argument value must be between {StartIndex} and {EndIndex}");
            return m_rawFetcher(idx);
        }

        
        /// <summary>
        /// Start (Minimum) index value that may be passed in to the FetchItem delegate.
        /// </summary>
        public int StartIndex { get; }
        /// <summary>
        /// End (Maximum) index value that may be passed in to the FetchItem delegate.
        /// </summary>
        public int EndIndex { get; }

        /// <summary>
        /// Returns current index of the cache item being tested for eviction.
        /// </summary>
        public int CurrentIndex { get; internal set; }

        public CacheItem<TValue, TKey> CurrentCacheItem => Fetcher(CurrentIndex);
        
        /// <summary>
        /// Pass in the index that has a value between StartIndex and EndIndex to fetch next Replacement delegate candidate item. 
        /// </summary>
        public Func<int, CacheItem<TValue, TKey>> Fetcher {get;}
        
        private int m_CommitItemWithIndexForEviction = -1;
        /// <summary>
        /// Set the value of this property to the index from <see cref="StartIndex"/> to <see cref="EndIndex"/> to indicate to stop any further
        /// calls to your implementation of <see cref="ReplacementStrategyHandler<TValue, TKey>/>"/>. This will be the 
        /// </summary>
        /// <remarks>If this property value is never set then <see cref="ListOfNextBestEvictionCandidateIDXs"/> list is checked.
        /// If a valid eviction candidate index is not specified then cache falls back to the default LRU algorithm.</remarks>
        public int CommitItemWithIndexForEvicton { get { return m_CommitItemWithIndexForEviction; }
            set { if (value >= StartIndex && value <= EndIndex) m_CommitItemWithIndexForEviction = value; }
        }


        private List<int> m_ListOfNextBestEvictionCandidateIDXs = null;

        /// <summary>
        /// Add index in the range from <see cref="StartIndex"/> to <see cref="EndIndex"/> to indicate those items might be next best candidates for eviction.
        /// </summary>
        /// <remarks>If this list has valid indexes and <see cref="CommitItemWithIndexForEvicton"/> was never set then the last item on the list will be evicted</remarks>
        public List<int> ListOfNextBestEvictionCandidateIDXs {
            get
            {
                if (m_ListOfNextBestEvictionCandidateIDXs == null)
                    m_ListOfNextBestEvictionCandidateIDXs = new List<int>();
                return m_ListOfNextBestEvictionCandidateIDXs;
            }
        }

        /// <summary>
        /// Returns true if this is the last call for the current scan and it's the last chance to make up your mind and set <see cref="CommitItemWithIndexForEvicton"/> value
        /// </summary>
        public bool IsLastCallForThisScan => CurrentIndex == EndIndex;
    }

    public class Config<TValue, TKey>
    {
        /// <summary>
        /// Defines minimum number of bits to build the address for cache items
        /// </summary>
        /// <remarks>If this number is used for cache sizeBits parameter then total number of cache rows will be equal to 32</remarks>
        public const byte MinCacheSizeBits = 5; // 32 rows minimum 

        /// <summary>
        /// Defines maximum number of bits to build the address for cache items
        /// </summary>
        /// <remarks>If this number is used for cache sizeBits parameter then total number of cache rows will be equal to 33,554,432</remarks>
        public const byte MaxCacheSizeBits = 25;


        /// <summary>
        /// Default number of seconds to wait until initial cache loader stops querying ICacheLoader for additional items
        /// </summary>
        public const int LoadCacheMaxSecondsDefault = 300;



        /// <summary>
        /// Configures N-way set associative cache with the size = 2 to the power of <see cref="sizeBits"/>
        /// </summary>
        /// <param name="sizeBits">The number of bits used to form cache item address. The <see cref="Size"/> = 2 to the power of <see cref="sizeBits"/></param>
        /// <remarks>Number of bits used to address cache sets = LOG2(<see cref="sizeBits"/>). Uses LRU item eviction logic.</remarks>
        public Config(byte sizeBits) : this(sizeBits, (byte)Math.Log(sizeBits, 2)) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeBits">The number of bits used to form a cache item address. The <see cref="Size"/> = 2 to the power of <see cref="sizeBits"/></param>
        /// <param name="setBits">Number of bits used to form a cache set address. The <see cref="ItemsPerSet"/> = 2 to the power of <see cref="setBits"/></param>
        /// <remarks>Uses LRU item eviction logic.</remarks>
        public Config(byte sizeBits, byte setBits) : this(sizeBits, setBits, false){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeBits">The number of bits used to form a cache item address. The <see cref="Size"/> = 2 to the power of <see cref="sizeBits"/></param>
        /// <param name="setBits">Number of bits used to form a cache set address. The <see cref="ItemsPerSet"/> = 2 to the power of <see cref="setBits"/></param>
        /// <param name="MRUstrategy">Pass in <see cref="true"/> to use the MRU eviction logic or <see cref="false"/> to use LRU logic.</param>
        public Config(byte sizeBits, byte setBits, bool MRUstrategy) : this(sizeBits, setBits, 
            MRUstrategy? ReplacementStrategy.MRU: ReplacementStrategy.LRU, null) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sizeBits"></param>
        /// <param name="setBits"></param>
        /// <param name="replacementStrategyHr"></param>
        public Config(byte sizeBits, byte setBits, Action<ReplacementArgs<TValue, TKey>> replacementStrategyHr) : 
            this(sizeBits, setBits, ReplacementStrategy.Custom, replacementStrategyHr) { }


        private Config(byte sizeBits, byte setBits, ReplacementStrategy strategy,
            Action<ReplacementArgs<TValue, TKey>> replacementStrategyHr) : base() {
            if (sizeBits < MinCacheSizeBits || sizeBits > MaxCacheSizeBits)
                throw new ArgumentOutOfRangeException($"sizeBits argument must have a value between {MinCacheSizeBits} and {MaxCacheSizeBits}");
            if (setBits < 0) throw new ArgumentOutOfRangeException(@"setBits cannot be less than 0");
            if (setBits > sizeBits) throw new ArgumentException(@"setBits argument value cannot be greater than value of sizeBits");

            if (strategy == ReplacementStrategy.Custom && replacementStrategyHr == null)
                throw new ArgumentNullException(@"replacementStrategyHr is reguired if custom replacement strategy is used,");

            Size = 2 << (sizeBits -1);
            ItemsPerSet = (setBits <= 0) ? Size: 2 << (setBits -1);
            if (setBits == 0) //one set. Fully associative
                CacheType = CachingAlgorithm.FullyAssociative;
            else if (setBits == sizeBits) //number of sets = cache size. Direct mapped
                CacheType = CachingAlgorithm.DirectMapped;
            else CacheType = CachingAlgorithm.SetAssociative;
            Strategy = strategy;
            ReplacementHandler = replacementStrategyHr;

        }

        /// <summary>
        /// Gets/Sets hash calculator handler to provide custom hashing logic that is specific to the size of
        /// cache and the data statistical knowledge.
        /// </summary>
        /// <remarks>Default hash calculation algorithm simply delegetes to <see cref="object.GetHashCode()"/>, so there is no need
        /// to specify the handler here if your <see cref="TValue"/> already overrides <see cref="TValue.GetHashCode()"/>.</remarks>
        public Func<TKey, int> HashCalculator { get; set; }


        /// <summary>
        /// Gets/Sets value loader handler to optionally load <see cref="TValue"/> using given <see cref="TKey"/>.
        /// </summary>
        /// <remarks>Client must provide implementation specific to the storage system used in a particular application.</remarks>
        public Func<TKey, TValue> LoadHandler { get; set; }


        /// <summary>
        /// Get/Sets custom cache item create handler that must return a custom implementation of the <see cref="CacheItem{TValue, TKey}"/> to better control
        /// Key and Value comparison operations.
        /// </summary>
        public Func<TValue, TKey, CacheItem<TValue, TKey>> CreateItemHandler { get; set; }


        /// <summary>
        /// Gets/Sets the number of minutes after which a cache item is considered stale.
        /// </summary>
        /// <remarks>If set to 0 or any negative number of of minutes then items in this cache will never expire</remarks>
        public int CacheExpirationMinutes { get; set; } = 0;

        internal long CacheExpirationTicks 
            => (CacheExpirationMinutes > 0) ? (new TimeSpan(0, CacheExpirationMinutes, 0).Ticks) : 0;


        /// <summary>
        /// Returns type of the caching algorithm used
        /// </summary>
        public CachingAlgorithm CacheType { get; }

        /// <summary>
        /// Returns replacement strategy type used
        /// </summary>
        public ReplacementStrategy Strategy { get; }

        internal const byte DefaultMaxNumberOfScanThreads = 4;
        /// <summary>
        /// Gets/Sets the max number of threads used for cache item scanning operations.
        /// </summary>
        /// <remars>Default value is 4</remars>
        public byte ThreadsMaxNumberOfScan { get; set; } = DefaultMaxNumberOfScanThreads;


        /// <summary>
        /// Minimum number of items per set to trigger the multiple threads scan.
        /// </summary>
        /// <remarks>Default value is 1024</remarks>
        public short ThreadsMinNumberItemsPerSlot { get; set; } = Cache<TValue, TKey>.MinItemsPerSetForMultithreadScan;


        /// <summary>
        /// Maximumn number of seconds to wait until initial cache loader stops querying <see cref="ICacheLoadProvider"/> for additional items.        
        /// </summary>
        ///<remarks>Setting this property value to a negative or zero will cause unlimited** wait time.
        ///Not recommended since hash code generation logic is not guaranteed to produce uniform distribution
        ///of int values.</remarks>
        public int LoadCacheMaxSeconds { get; set; } = LoadCacheMaxSecondsDefault;

        internal Action<ReplacementArgs<TValue, TKey>> ReplacementHandler;
        
        /// <summary>
        /// Returns the number of bits used to form cache item address.
        /// </summary>
        /// <remarks>The <see cref="Size"/> = 2 to the power of <see cref="SizeBits"/></remarks>
        public byte SizeBits { get; }

        /// <summary>
        ///  Returns the number of bits used to form the address of chache sets.
        /// </summary>
        /// <remarks>The <see cref="ItemsPerSet"/> = 2 to the power of <see cref="SetSizeBits"/></remarks>
        public byte SetSizeBits { get; }

        /// <summary>
        /// Returns the size of cache as a max number of cache items.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Returns the size of cache set in a number of items per set.
        /// </summary>
        public int ItemsPerSet { get; }

        /// <summary>
        /// Returns the number of cache sets used for this cache configuration.
        /// </summary>
        public int NumberOfSets { get { return Size / ItemsPerSet; } }

    }
}
