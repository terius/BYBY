using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BYBY.Repository.DataContextStorage
{
    public interface IDataContextStorageContainer
    {
        BYBYDBContext GetDataContext();
        void Store(BYBYDBContext libraryDataContext);

    
    }
}
