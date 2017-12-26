using BYBY.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BYBY.Repository.Entities
{
    public class TBCheckAssay : BaseEntity<int>
    {
        public TBCheckAssay()
        {
            this.ConsultationChecks = new HashSet<TBConsultationCheck>();
        }
        /// <summary>
        /// 项目分类编码
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 项目分类名称
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Name { get; set; }


        /// <summary>
        /// 化验说明
        /// </summary>
        public AssayType? AssayType { get; set; }


        /// <summary>
        ///  所属上级项目
        /// </summary>
        public int? ParentItem { get; set; }


        /// <summary>
        /// 有效时长
        /// </summary>
        public int? EffectiveTime { get; set; }


        /// <summary>
        /// 模式
        /// </summary>
        public CheckMode? CheckMode { get; set; }

        public virtual ICollection<TBConsultationCheck> ConsultationChecks { get; set; }


    }
}
