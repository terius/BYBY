using BYBY.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace BYBYApp.Models
{
    public class MedicalHistoryAddModel
    {

        public MedicalHistoryAddModel()
        {
            AddModel = new MedicalHistoryAddRequest();
            AddModel.CardType = BYBY.Infrastructure.CardType.HuZhao;
        }
        public IList<ListItem> CardTypeList { get; set; }


        public MedicalHistoryAddRequest AddModel { get; set; }
    }
}