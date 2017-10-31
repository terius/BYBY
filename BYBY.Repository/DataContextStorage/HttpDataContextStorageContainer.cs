using System.Web;

namespace BYBY.Repository.DataContextStorage
{
    public class HttpDataContextStorageContainer : IDataContextStorageContainer 
    {
        private string _dataContextKey = "DataContext";

        public BYBYDBContext GetDataContext()
        {
            BYBYDBContext objectContext = null;
            if (HttpContext.Current == null)
            {
                return new BYBYDBContext();
            }
            if (HttpContext.Current.Items.Contains(_dataContextKey))
            {
                objectContext = (BYBYDBContext)HttpContext.Current.Items[_dataContextKey];
            }
            return objectContext;
        }

        public void Store(BYBYDBContext libraryDataContext)
        {
            if (HttpContext.Current.Items.Contains(_dataContextKey))
                HttpContext.Current.Items[_dataContextKey] = libraryDataContext;
            else
                HttpContext.Current.Items.Add(_dataContextKey, libraryDataContext);  
        }
    }
}
