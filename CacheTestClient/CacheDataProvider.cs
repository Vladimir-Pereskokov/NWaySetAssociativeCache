using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSetACache;
using NSetACache.Interfaces;
using System.Collections.ObjectModel;

namespace CacheTestClient
{
    /// <summary>
    /// Implements custom initial cache load functionality
    /// </summary>
    class CacheDataProvider : ICacheLoadProvider<OrderDetail, int>, IDisposable {
        private ReadOnlyCollection<OrderDetail> m_lst = null;
        private string m_sourceFile = null;
        private int m_CurrentIDX = -1;
        private int m_maxInitialRecords = 0;
        private int m_RecordsReturned = 0;

        /// <summary>
        /// Instantiates new cache data provider
        /// </summary>
        /// <param name="sourceFile">data storage file</param>
        /// <param name="maxInitialRecords">max number of test records to load</param>
        internal CacheDataProvider(string sourceFile, int maxInitialRecords)
        {
            m_sourceFile = sourceFile;
            m_maxInitialRecords = maxInitialRecords;
        }

        /// <summary>
        /// Returns next key (int) value
        /// </summary>
        /// <param name="recordsLoaded"></param>
        /// <param name="cfg"></param>
        /// <param name="setQry"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetNextKey(int recordsLoaded, Config<OrderDetail, int> cfg, Func<int, int> setQry, out int key)
        {
            var list = MyList;
            if (list != null)
            {                
                m_CurrentIDX += 1;
                if (m_CurrentIDX < list.Count) {
                    key = list[m_CurrentIDX].RowIndex;
                    return true;
                } 
            }
            key = -1;
            return false;
        }

        /// <summary>
        /// Returns Order detail object for a given row index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public OrderDetail GetValue(int key)
        {
            var list = MyList;
            
            if (list != null)
            {                
                if (key < list.Count)
                {
                    var keyCurr = list[key].RowIndex;
                    if (keyCurr == key) {
                        m_RecordsReturned++; if (m_RecordsReturned >= m_maxInitialRecords) Dispose(); return list[key];
                    }
                    else if (keyCurr > key)
                    {
                        for (var idx = key - 1; idx > -1; idx--)
                        {
                            keyCurr = list[idx].RowIndex;
                            if (keyCurr == key)  {
                                m_RecordsReturned++; if (m_RecordsReturned >= m_maxInitialRecords) Dispose(); return list[idx];
                            }
                        }
                    }
                    else if (keyCurr < key)
                    {
                        for (var idx = key + 1; idx < list.Count; idx++)
                        {
                            keyCurr = list[idx].RowIndex;
                            if (keyCurr == key) {
                                m_RecordsReturned++; if (m_RecordsReturned >= m_maxInitialRecords) Dispose(); return list[idx];
                            }

                        }
                    }
                }
                else if (m_CurrentIDX < list.Count)
                {
                    if (key == list[m_CurrentIDX].RowIndex)
                    {
                        m_RecordsReturned++; if (m_RecordsReturned >= m_maxInitialRecords) Dispose(); return list[m_CurrentIDX];                        
                    }
                }
            }
            return null;
        }


        private ReadOnlyCollection<OrderDetail> MyList {
            get
            {
                if (m_lst == null && !string.IsNullOrEmpty(m_sourceFile)) m_lst = OrderDetail.GetList(m_sourceFile);                             
                return m_lst;
            }
        }


        public void Dispose() {
            m_lst = null;
            m_sourceFile = null;
        }

    }
}
