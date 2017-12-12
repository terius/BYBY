using BYBY.Infrastructure;
using BYBY.Services.Request;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class MedicalHistoryDetailModel
    {

        public MedicalHistoryDetailModel()
        {
            EditModel = new MedicalHistoryEditRequest();
        }
        public IList<SelectItem> CardTypeList { get; set; }

        public IList<SelectItem> MarriageList { get; set; }

        public IList<SelectItem> EducationList { get; set; }


        public IList<SelectItem> NationList { get; set; }

        public IList<SelectItem> JobList { get; set; }

        public IList<SelectItem> EthnicList { get; set; }




        public MedicalHistoryEditRequest EditModel { get; set; }

        public IList<MedicalDetailRequest> FemaleMedicalDetails { get; set; }

        public IList<MedicalDetailRequest> MaleMedicalDetails { get; set; }
    }
}