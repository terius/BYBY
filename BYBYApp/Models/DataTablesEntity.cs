using System.Collections.Generic;

namespace BYBYApp.Models
{
    public class DataTablesEntity<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IList<T> data { get; set; }
    }
}