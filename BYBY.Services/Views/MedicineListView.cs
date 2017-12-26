namespace BYBY.Services.Views
{
    public class MedicineListView 
    {
        public int Id { get; set; }
        /// <summary>
        /// 医嘱编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 药品分类
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ShortName { get; set; }


        /// <summary>
        /// 药品规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public int Dose { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        public string Frequency { get; set; }


        /// <summary>
        /// 使用方法
        /// </summary>
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


        /// <summary>
        /// 注射标记
        /// </summary>
        public string InjectionMarkText { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public string IsUsedText { get; set; }


    }
}
