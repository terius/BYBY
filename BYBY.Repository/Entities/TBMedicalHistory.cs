using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBMedicalHistory : BaseEntity<int>
    {
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
    }
}
