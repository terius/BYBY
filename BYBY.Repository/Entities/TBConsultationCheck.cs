using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    /// <summary>
    /// 会诊检查项目
    /// </summary>
    public class TBConsultationCheck : BaseEntity<int>
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public int CheckId { get; set; }

        /// <summary>
        /// 会诊Id
        /// </summary>
        public int ConsultationId { get; set; }

        


        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime ActionDate { get; set; }


        [ForeignKey("CheckId")]
        public virtual TBCheckAssay CheckAssay { get; set; }


        [ForeignKey("ConsultationId")]
        public virtual TBConsultation Consultation { get; set; }
    }
}
