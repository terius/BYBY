namespace BYBY.Repository
{
    public class DataContextFactory
    {
        static BYBYDBContext _context;

        public static BYBYDBContext GetDataContext()
        {
            if (_context == null)
            {
                _context = new BYBYDBContext();
                
            }
            return _context;
        }


    }

}
