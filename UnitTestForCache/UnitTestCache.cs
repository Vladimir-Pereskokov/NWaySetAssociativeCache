using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSetACache;


namespace UnitTestForCache
{
    [TestClass]
    public class UnitTestCache
    {
        [TestMethod]
        public void CanCreate()
        {
            var cache = new Cache<string, string>(new Config<string, string>(5, 0));
            Assert.IsInstanceOfType(cache, typeof(Cache<string, string>));
        }


        [TestMethod]
        public void Can_set_and_get_objects_from_cache()
        {
            var cache = new Cache<string, string>(new Config<string, string>(5, 0));

            cache.PushItem("value-1", "key-1");
            cache.PushItem("value-3", "key-3");

            var value1Found = cache.PullItem("key-1", out var value1);
            Assert.IsTrue(value1Found);
            Assert.AreEqual("value-1", value1);

            var value2Found = cache.PullItem("key-2", out var value2);
            Assert.IsFalse(value2Found);

            var value3Found = cache.PullItem("key-3", out var value3);
            Assert.IsTrue(value3Found);
            Assert.AreEqual("value-3", value3);
        }



        [TestMethod]
        public void Can_use_custom_eviction_strategy() {
            Config<string, string> cfg = null;

            //make sure we can't create cache with the size less than 32
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => createInvalidConfig(false));

            //make sure we can't create cache with size greater than 2 to the power of 25
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => createInvalidConfig(true));

            void createInvalidConfig(bool biggerThanMax) {
                cfg = new Config<string, string>(
                    (byte)(biggerThanMax? Config<string, string>.MaxCacheSizeBits + 1: 
                    Config<string, string>.MinCacheSizeBits-1));
            };


            cfg = new Config<string, string>(5, 0,
                CustomEvictionStrategy.StringReplacementStrategy);
            var cache = new Cache<string, string>(cfg);
            
            for (int idx = 1; idx < 2 * cfg.Size; idx++) {
                var sKey = "key-" + idx;
                if (idx % 2 > 0) //odd                    
                {
                    var val = "value" + idx;
                    cache.PushItem(val, sKey);
                    Assert.IsTrue(cache.PullItem(sKey, out var valOdd));
                    Assert.AreEqual<string>(val, valOdd);
                }
                else Assert.IsFalse(cache.PullItem(sKey, out _)); //even
            }           

        }
    }
}
