using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBMasterHospital : BaseEntity<int>
    {
        public TBMasterHospital()
        {
            
        }

        public int ChildHospitalId { get; set; }

        public int MasterHospitalId { get; set; }


        [StringLength(500)]
        public string Remark { get; set; }

        [ForeignKey("ChildHospitalId")]
        public virtual TBHospital ChildHospital { get; set; }

        [ForeignKey("MasterHospitalId")]
        public virtual TBHospital MasterHospital { get; set; }

    }
}
