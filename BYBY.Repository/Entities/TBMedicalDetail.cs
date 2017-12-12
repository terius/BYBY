using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BYBY.Repository.Entities
{
    public class TBMedicalDetail : BaseEntity<int>
    {
        public TBMedicalDetail()
        {

        }

        [Required]
        public int PatientId { get; set; }

        /// <summary>
        ///现病史-主诉
        /// </summary>
        [StringLength(100)]
        public string CurrentInfoMain { get; set; }

        /// <summary>
        ///现病史
        /// </summary>
        [StringLength(100)]
        public string CurrentInfo { get; set; }

        /// <summary>
        /// 肝炎
        /// </summary>
        [StringLength(100)]
        public string PastHepatitis { get; set; }

        /// <summary>
        /// 手术史
        /// </summary>
        [StringLength(100)]
        public string PastSurgery { get; set; }

        /// <summary>
        /// 结核病
        /// </summary>
        [StringLength(100)]
        public string PastTuberculosis { get; set; }

        /// <summary>
        /// 泌尿系统疾病
        /// </summary>
        [StringLength(100)]
        public string PastUrinarySystemDisease { get; set; }

        /// <summary>
        /// 心血管疾病
        /// </summary>
        [StringLength(100)]
        public string PastCardiovascularDisease { get; set; }

        /// <summary>
        /// 盆腔炎
        /// </summary>
        [StringLength(100)]
        public string PastPelvicInflammatoryDisease { get; set; }

        /// <summary>
        /// 性病
        /// </summary>
        [StringLength(100)]
        public string PastSTD { get; set; }

        /// <summary>
        /// 肾病
        /// </summary>
        [StringLength(100)]
        public string PastKidneyDisease { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        [StringLength(200)]
        public string PastOther { get; set; }


        #region 个人史

        /// <summary>
        /// 吸烟
        /// </summary>
        [StringLength(100)]
        public string PersonalSmoke { get; set; }


        /// <summary>
        /// 酗酒
        /// </summary>
        [StringLength(100)]
        public string PersonalAlcoholism { get; set; }


        /// <summary>
        /// 吸毒
        /// </summary>
        [StringLength(100)]
        public string PersonalDrug { get; set; }


        /// <summary>
        /// 习惯用药
        /// </summary>
        [StringLength(100)]
        public string PersonalHabitMedication { get; set; }

        /// <summary>
        /// 药物过敏
        /// </summary>
        [StringLength(100)]
        public string PersonalDrugAllergy { get; set; }

        #endregion




        /// <summary>
        /// 月经史初潮
        /// </summary>
        public int? MenstruationFirstAge { get; set; }

        /// <summary>
        /// 月经周期（天）
        /// </summary>
        public int? MenstruationCycle { get; set; }

        /// <summary>
        /// 月经持续时间（天）
        /// </summary>
        public int? MenstruationDuration { get; set; }

        /// <summary>
        /// 月经量
        /// </summary>
        [StringLength(20)]
        public string MenstruationVolume { get; set; }

        /// <summary>
        /// 痛经
        /// </summary>
        [StringLength(100)]
        public string MenstruationDysmenorrhea { get; set; }

        /// <summary>
        /// 血块
        /// </summary>
        [StringLength(100)]
        public string MenstruationGore { get; set; }

        /// <summary>
        /// 上次月经
        /// </summary>
        public DateTime? MenstruationLast { get; set; }

        /// <summary>
        /// 婚育史-是否近亲结婚
        /// </summary>
        [StringLength(100)]
        public string MarriageRelatives { get; set; }

        /// <summary>
        /// 婚育史-再婚
        /// </summary>
        [StringLength(100)]
        public string MarriageRemarry { get; set; }

        /// <summary>
        /// 婚育史-上次妊娠时间
        /// </summary>
        public DateTime? MarriageLastPregnancyDate { get; set; }

        /// <summary>
        /// 婚育史-子女
        /// </summary>
        [StringLength(100)]
        public string MarriageChildren { get; set; }


        /// <summary>
        /// 婚育史-生育
        /// </summary>
        [StringLength(200)]
        public string MarriageFertility { get; set; }

        /// <summary>
        /// 婚育史-宫外孕（异位妊娠）
        /// </summary>
        public int? MarriageEctopicPregnancy { get; set; }

        /// <summary>
        /// 婚育史-手术名称及时间
        /// </summary>
        [StringLength(200)]
        public string MarriageSurgeryAndDate { get; set; }



        /// <summary>
        /// 体格检查-身高（cm）
        /// </summary>
        public int PhysiqueHeight { get; set; }


        /// <summary>
        /// 体格检查-身高（kg）
        /// </summary>
        public int PhysiqueWeight { get; set; }

        /// <summary>
        /// 体格检查-体重指数
        /// </summary>
        public double PhysiqueBMI { get; set; }



        /// <summary>
        /// 妇科检查-外阴
        /// </summary>
        [StringLength(100)]
        public string GynecologyVulva { get; set; }


        /// <summary>
        /// 妇科检查-阴道
        /// </summary>
        [StringLength(100)]
        public string GynecologyVaginal { get; set; }

        /// <summary>
        /// 妇科检查-宫颈
        /// </summary>
        [StringLength(100)]
        public string GynecologyCervix { get; set; }

        /// <summary>
        /// 妇科检查-宫体
        /// </summary>
        [StringLength(100)]
        public string GynecologyCervixBody { get; set; }

        /// <summary>
        /// 妇科检查-双附件
        /// </summary>
        [StringLength(100)]
        public string GynecologyDoubleAtt { get; set; }

        /// <summary>
        /// 治疗意见-诊断
        /// </summary>
        [StringLength(100)]
        public string TreatmentAdviceDiagnosis { get; set; }

        /// <summary>
        /// 治疗意见
        /// </summary>
        [StringLength(100)]
        public string TreatmentAdvice { get; set; }

        /// <summary>
        /// 诊断医生
        /// </summary>
        public int DiagnosisDoctorId { get; set; }

        [ForeignKey("PatientId")]
        public virtual TBPatient Patient { get; set; }

    }
}
