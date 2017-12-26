using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    /// <summary>
    /// 会诊用药列表
    /// </summary>
    public class TBConsultationMedicine : BaseEntity<int>
    {
        /// <summary>
        /// 药品Id
        /// </summary>
        public int MedicineId { get; set; }

        /// <summary>
        /// 会诊Id
        /// </summary>
        public int ConsultationId { get; set; }

        /// <summary>
        /// 用药天数
        /// </summary>
        public int UsedDays { get; set; }


        /// <summary>
        /// 总剂量
        /// </summary>
        public int AllDose { get; set; }


        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime ActionDate { get; set; }


        [ForeignKey("MedicineId")]
        public virtual TBMedicine Medicine { get; set; }


        [ForeignKey("ConsultationId")]
        public virtual TBConsultation Consultation { get; set; }
    }
}
