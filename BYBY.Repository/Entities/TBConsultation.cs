using BYBY.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    /// <summary>
    /// 会诊表
    /// </summary>
    public class TBConsultation : BaseEntity<int>
    {
        /// <summary>
        /// 病历Id
        /// </summary>
        public int TBMedicalHistoryId { get; set; }


        /// <summary>
        /// 会诊医院
        /// </summary>
        public int HospitalId { get; set; }

        /// <summary>
        /// 申请会诊日期
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 会诊医生
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// 会诊时间段（开始时间）
        /// </summary>
        public DateTime STime { get; set; }

        /// <summary>
        /// 会诊时间段（结束时间）
        /// </summary>
        public DateTime ETime { get; set; }


        /// <summary>
        /// 会诊状态
        /// </summary>
        public ConsultationStatus ConsultationStatus { get; set; }


        /// <summary>
        /// 申请备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }
        [ForeignKey("DoctorId")]
        public virtual TBDoctor Doctor { get; set; }

        [ForeignKey("TBMedicalHistoryId")]
        public virtual TBMedicalHistory MedicalHistory { get; set; }
    }
}
