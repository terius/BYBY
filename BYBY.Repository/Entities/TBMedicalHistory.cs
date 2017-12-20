using BYBY.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBMedicalHistory : BaseEntity<int>
    {
        public TBMedicalHistory()
        {
            this.TBMedicalHistoryImages = new HashSet<TBMedicalHistoryImage>();
            this.Consultations = new HashSet<TBConsultation>();
            this.Referrals = new HashSet<TBReferral>();
        }


        [Required]
        [StringLength(30)]
        public string MedicalHistoryNo { get; set; }

        [Required]
        public int MalePatientId { get; set; }

        [Required]
        public int FeMalePatientId { get; set; }

       
        [ForeignKey("MalePatientId")]
        public virtual TBPatient MalePatient { get; set; }

        [ForeignKey("FeMalePatientId")]
        public virtual TBPatient FeMalePatient { get; set; }

        [StringLength(30)]
        public string LandlinePhone { get; set; }

        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 会诊状态
        /// </summary>
        public ConsultationStatus ConsultationStatus { get; set; }

        /// <summary>
        /// 转诊状态
        /// </summary>
        public ReferralStatus ReferralStatus { get; set; }

        protected override void Validate()
        {
            if (dbaction == DBAction.Add)
            {
                this.ConsultationStatus = ConsultationStatus.No;
                this.ReferralStatus = ReferralStatus.No;
            }
            base.Validate();
        }

        public virtual ICollection<TBMedicalHistoryImage> TBMedicalHistoryImages { get; set; }

        public virtual ICollection<TBConsultation> Consultations { get; set; }

        public virtual ICollection<TBReferral> Referrals { get; set; }
    }
}
