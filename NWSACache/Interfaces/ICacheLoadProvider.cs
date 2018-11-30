using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSetACache.Interfaces
{
    /// <summary>
    /// Defines the interface that is used to load cache with initial cache data.
    /// </summary>
    /// <typeparam name="TValue">represents Value type</typeparam>
    /// <typeparam name="TKey">represents Key type</typeparam>
    public interface ICacheLoadProvider <TValue, TKey>
    {      
        /// <summary>
        /// Must return true if provider has successfully completed next Key value retrieval.
        /// If provider returns false then the cache load process stops.
        /// </summary>
        /// <param name="recordsLoaded">The number of the cache records that have been loaded so far.</param>
        /// <param name="cfg">cache configuration object that may be usefull for the provider to make a decision if to continue
        /// or stor the process.</param>
        /// <param name="setAddressFromSetNumberHandler">set address delegate. May be used to provide the load progress feedback</param>
        /// <param name="key">Returns the next key value. Must be unique throughout the store</param>
        /// <returns></returns>
        bool GetNextKey(int recordsLoaded, Config<TValue, TKey> cfg, 
                                Func<int, int> setAddressFromSetNumberHandler, out TKey key);
        /// <summary>
        /// Returns Value instance for a given Key
        /// </summary>
        /// <param name="key">Key to load the Value for</param>
        /// <returns></returns>
        TValue GetValue(TKey key);
    }
}
