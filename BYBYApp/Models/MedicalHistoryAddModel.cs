using BYBY.Infrastructure;
using BYBY.Services.Request;
using System.Collections.Generic;

namespace BYBYApp.Models
{
    public class MedicalHistoryAddModel
    {

        public MedicalHistoryAddModel()
        {
            AddModel = new MedicalHistoryAddRequest();
            //  AddModel.FemaleCardType = CardType.SFZ;
            //   AddModel.FemaleCardType = BYBY.Infrastructure.CardType.HuKouBen;
            //    AddModel.MedicalHistoryNo = "9999";
            //  AddModel.FemaleBirthday = "1980-10-10";
            //    AddModel.FemaleNationId = 5;
        }
        public IList<SelectItem> CardTypeList { get; set; }

        public IList<SelectItem> MarriageList { get; set; }

        public IList<SelectItem> EducationList { get; set; }


        public IList<SelectItem> NationList { get; set; }

        public IList<SelectItem> JobList { get; set; }

        public IList<SelectItem> EthnicList { get; set; }




        public MedicalHistoryAddRequest AddModel { get; set; }

        public int DefaultCardType { get; set; }
    }
}