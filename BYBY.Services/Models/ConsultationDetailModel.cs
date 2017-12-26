using BYBY.Infrastructure;
using BYBY.Services.Request;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class ConsultationDetailModel
    {

        public ConsultationDetailModel()
        {
       
        }
        public int Id { get; set; }


        public string FemaleName { get; set; }

        public string FemaleAge { get; set; }

        public string MaleName { get; set; }

        public string MaleAge { get; set; }

        public string RequestUser { get; set; }

        public string RequestDate { get; set; }

        public string Remark { get; set; }

        public string ConsultationStatus { get; set; }

        public string Hospital { get; set; }

        public string ApprovedUser { get; set; }

        public string ApprovedTime { get; set; }


        /// <summary>
        /// 诊断
        /// </summary>
        public string Diagnosis { get; set; }


        /// <summary>
        /// 治疗意见
        /// </summary>
        public string TreatmentAdvice { get; set; }

        /// <summary>
        /// 治疗备注
        /// </summary>
        public string TreatmentRemark { get; set; }


        /// <summary>
        /// 会诊医生
        /// </summary>
        public string Doctor { get; set; }


        /// <summary>
        /// 会诊医生Id
        /// </summary>
        public int DoctorId { get; set; }


        /// <summary>
        /// 记录人
        /// </summary>
        public string RecordUser { get; set; }


        /// <summary>
        /// 记录时间
        /// </summary>
        public string RecordTime { get; set; }


        /// <summary>
        /// 医生列表
        /// </summary>
        public IList<SelectItem> DoctorList { get; set; }


        /// <summary>
        /// 使用药品信息列表
        /// </summary>
        public IList<ConsultationMedicineListRequest> ConsultationMedicineList { get; set; }

        /// <summary>
        /// 药品列表
        /// </summary>
        public IList<SelectItem> MedicineList { get; set; }



        /// <summary>
        /// 检查项列表
        /// </summary>
        public IList<ConsultationCheckListRequest> ConsultationCheckList { get; set; }



        /// <summary>
        /// 检查项列表
        /// </summary>
        public IList<SelectItem> CheckList { get; set; }


    }
}