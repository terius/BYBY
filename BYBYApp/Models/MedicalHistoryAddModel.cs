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
            //   AddModel.FemaleCardType = BYBY.Infrastructure.CardType.HuKouBen;
            //    AddModel.MedicalHistoryNo = "9999";
          //  AddModel.FemaleBirthday = "1980-10-10";
        }
        public IList<SelectItem> CardTypeList { get; set; }

        public IList<SelectItem> MarriageList { get; set; }


        public MedicalHistoryAddRequest AddModel { get; set; }
    }
}