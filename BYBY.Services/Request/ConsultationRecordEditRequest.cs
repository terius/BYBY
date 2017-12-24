using System;

namespace BYBY.Services.Request
{
    public class ConsultationRecordEditRequest
    {
        public int MyId { get; set; }
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
        public int DoctorId { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string RecordUser { get; set; }


        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? RecordTime { get; set; }
    }
}
