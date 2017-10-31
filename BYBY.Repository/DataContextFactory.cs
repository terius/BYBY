using BYBY.Repository.DataContextStorage;


namespace BYBY.Repository
{
    public class DataContextFactory
    {
        public static BYBYDBContext GetDataContext()
        {
          //  DateTime dt1 = DateTime.Now;
            IDataContextStorageContainer _dataContextStorageContainer = DataContextStorageFactory.CreateStorageContainer();
            BYBYDBContext BYBYDBContext = _dataContextStorageContainer.GetDataContext();
            if (BYBYDBContext == null)
            {
                BYBYDBContext = new BYBYDBContext();
                _dataContextStorageContainer.Store(BYBYDBContext);
            }
           // var ts = DateTime.Now.Subtract(dt1).TotalSeconds.ToString("0.00");
           // BYBY.Infrastructure.Logging.LoggingFactory.GetLogger().Log("加载Context时间"+ ts + "秒");
            return BYBYDBContext;
        }

     
    }

}
