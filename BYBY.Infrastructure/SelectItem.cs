using System.Collections.Generic;

namespace BYBY.Infrastructure
{
    public class SelectItem
    {
        public string id { get; set; }
        public string text { get; set; }

        public string title { get; set; }

        public string parent { get; set; }

        public IList<SelectItem> children { get; set; }
    }
}
