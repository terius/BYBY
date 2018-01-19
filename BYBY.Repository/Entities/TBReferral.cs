using BYBY.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    /// <summary>
    /// 转诊表
    /// </summary>
    public class TBReferral : BaseEntity<int>
    {
        /// <summary>
        /// 病历Id
        /// </summary>
        public int TBMedicalHistoryId { get; set; }


        /// <summary>
        /// 转诊医院
        /// </summary>
        public int HospitalId { get; set; }

        /// <summary>
        /// 申请转诊日期
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 转诊医生
        /// </summary>
        public int DoctorId { get; set; }


        /// <summary>
        /// 转诊状态
        /// </summary>
        public ReferralStatus ReferralStatus { get; set; }

        /// <summary>
        /// 申请备注
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        [ForeignKey("HospitalId")]
        public virtual TBHospital Hospital { get; set; }
     

        [ForeignKey("TBMedicalHistoryId")]
        public virtual TBMedicalHistory MedicalHistory { get; set; }

        [ForeignKey("DoctorId")]
        public virtual TBDoctor Doctor { get; set; }
    }
}
