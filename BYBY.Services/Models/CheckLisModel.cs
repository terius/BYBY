using BYBY.Infrastructure;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class CheckListModel
    {
        public IList<SelectItem> AssayTypeList { get; set; }

        public IList<SelectItem> CheckModeList { get; set; }
    }
}
