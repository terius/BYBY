using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;

namespace BYBY.Repository.DataContextStorage
{
    public class ThreadDataContextStorageContainer : IDataContextStorageContainer
    {
        private static readonly Hashtable _libraryDataContexts = new Hashtable();

        public BYBYDBContext GetDataContext()
        {
            BYBYDBContext libraryDataContext = null;
            if (_libraryDataContexts.Contains(GetThreadName()))
            {
                libraryDataContext = (BYBYDBContext)_libraryDataContexts[GetThreadName()];
            }
            return libraryDataContext;
        }

        public void Store(BYBYDBContext libraryDataContext)
        {
            if (_libraryDataContexts.Contains(GetThreadName()))
                _libraryDataContexts[GetThreadName()] = libraryDataContext;
            else
                _libraryDataContexts.Add(GetThreadName(), libraryDataContext);
        }


        

        private static string GetThreadName()
        {
            return Thread.CurrentThread.Name ?? Guid.NewGuid().ToString();
        }
    }
}
