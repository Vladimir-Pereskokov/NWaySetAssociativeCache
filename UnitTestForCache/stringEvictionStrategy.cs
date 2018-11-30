using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestForCache
{
    internal static class CustomEvictionStrategy {         
        internal static void StringReplacementStrategy(NSetACache.ReplacementArgs<string, string> args) {
            var currentItem = args.CurrentCacheItem;
            List<int> lst = args.ListOfNextBestEvictionCandidateIDXs;            
            if (currentItem != null && !string.IsNullOrEmpty(currentItem.Value))
            {
                string prevValue = (lst.Count > 0) ? args.Fetcher(lst[0]).Value : null;
                if (!string.IsNullOrEmpty(prevValue) && 
                    prevValue.Length > currentItem.Value.Length) lst[0] = args.CurrentIndex;                
            }
            else {
                if (lst.Count > 0) lst[0] = args.CurrentIndex;
                else lst.Add(args.CurrentIndex);
            }
            if (args.IsLastCallForThisScan && lst.Count > 0) args.CommitItemWithIndexForEvicton = lst[0];
        }
    }
}
