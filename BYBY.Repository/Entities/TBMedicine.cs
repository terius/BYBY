using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBMedicine : BaseEntity<int>
    {
        public TBMedicine()
        {
            this.ConsultationMedicines = new HashSet<TBConsultationMedicine>();
        }
        /// <summary>
        /// 医嘱编码
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// 药品分类
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [StringLength(100)]
        [Required]
        public string ShortName { get; set; }


        /// <summary>
        /// 药品规格
        /// </summary>
        [StringLength(100)]
        public string Spec { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public int Dose { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(100)]
        public string Unit { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        [StringLength(100)]
        public string Frequency { get; set; }


        /// <summary>
        /// 使用方法
        /// </summary>
        [StringLength(200)]
        public string Instructions { get; set; }


        /// <summary>
        /// 默认用药天数
        /// </summary>
        public int DefaultUsedDay { get; set; }

        /// <summary>
        /// 注射标记
        /// </summary>
        public bool InjectionMark { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUsed { get; set; }


        public virtual ICollection<TBConsultationMedicine> ConsultationMedicines { get; set; }


    }
}
