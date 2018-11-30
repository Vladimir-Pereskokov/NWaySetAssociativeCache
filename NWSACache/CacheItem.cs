using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSetACache
{

    /// <summary>
    /// Defines possible states of the cache item object
    /// </summary>
    public enum CacheItemState  : byte
    {   Missing = 0,
        Stale = 1,
        Ok =2,
        PlaceHolder =3
    }


    /// <summary>
    /// Defines Cache Item type that holds reference to both Value and a Key
    /// </summary>
    /// <typeparam name="TValue">Type of the Value item to store in cache</typeparam>
    /// <typeparam name="TKey">Type of the Key to pull value out of cache</typeparam>
    public class CacheItem<TValue, TKey>
    {
        /// <summary>
        /// Instantiates new cache item
        /// </summary>
        /// <param name="value">Value item</param>
        /// <param name="key">Key</param>
        public CacheItem(TValue value, TKey key) {
            Value = value;
            Key = key;            
            TimeStamp = DateTime.UtcNow.Ticks;
        }

        /// <summary>
        /// Returns empty item (PlaceHolder)
        /// </summary>
        /// <returns></returns>
        internal static CacheItem<TValue, TKey> CreateEmpty() => new CacheItem<TValue, TKey>(default(TValue), 
            default(TKey)) {State = CacheItemState.PlaceHolder};

        /// <summary>
        /// Returns the State of this cache item
        /// </summary>
        public CacheItemState State { get; internal set; }
        
        /// <summary>
        /// Returns cache Value item
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Returns Cache Key value
        /// </summary>
        public TKey Key { get; }

        #region "Overrides"

        /// <summary>
        /// Override this function if there is a TValue - specific knowlwdge of equavilty
        /// </summary>
        /// <param name="other">The other value to compare the internal value to</param>
        /// <returns></returns>
        /// <remarks>There is no need in overriding this function in case TValue is a primitive type or TValue already overrides Equals</remarks>
        protected internal virtual bool ValueEqualsTo(TValue other)        {
            return (other == null) ? (Value == null) : ((Value == null) ? false : other.Equals(Value));           
        }


        /// <summary>
        /// Override this function if there is a TKey - specific knowlwdge of key equavilty
        /// </summary>
        /// <param name="other">The other key to compare the internal key to</param>
        /// <returns></returns>
        /// <remarks>There is no need in overriding this function in case TKey is a primitive type or TKey already overrides Equals</remarks>
        protected internal virtual bool KeyEqualsTo(TKey other)
        {
            return (other == null) ? (Key == null) : ((Key == null) ? false : other.Equals(Key));
        }

       
        /// <summary>
        /// Override this function to provide TValue-type specific hashing algorithm based on cache
        /// search performance requirements and statistical data about the nature of TValue data.
        /// </summary>
        /// <returns>Internal value's hash code</returns>
        /// <remarks>This function is called by the parent cache only in case this item was added
        /// without explicit reference to the TKey value</remarks>
        protected internal virtual int OnGetItemHash()        {
            return (Value == null)? 0: Value.GetHashCode();
        }
       
        #endregion        
        /// <summary>
        /// Stores time stamp as number of ticks
        /// </summary>
        protected internal long TimeStamp;

        /// <summary>
        /// Item is valid only when there a key
        /// </summary>
        protected internal bool IsValid => Key != null;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
                return (obj is CacheItem<TValue, TKey>) && ValueEqualsTo(((CacheItem<TValue, TKey>)obj).Value);            
        }
        
        public override int GetHashCode()=> OnGetItemHash();

        public override string ToString() => (Value == null) ? "" : Value.ToString();        

    }   

}
