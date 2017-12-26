namespace BYBY.Services.Views
{
    public class ConsultationListView
    {
        public int Id { get; set; }

        /// <summary>
        /// 女方姓名
        /// </summary>
        public string FemaleName { get; set; }
        /// <summary>
        /// 女方年龄
        /// </summary>
        public int FemaleAge { get; set; }
        /// <summary>
        /// 男方姓名
        /// </summary>
        public string MaleName { get; set; }
        /// <summary>
        /// 男方年龄
        /// </summary>
        public int MaleAge { get; set; }

      
        /// <summary>
        /// 会诊状态
        /// </summary>
        public string ConsultationStatus { get; set; }


        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }
        /// <summary>
        /// 添加用户
        /// </summary>
        public string AddUser { get; set; }
        /// <summary>
        /// 会诊医院
        /// </summary>
        public string Hospital { get; set; }
        /// <summary>
        /// 申请会诊日期
        /// </summary>
        public string RequestDate { get; set; }
        /// <summary>
        /// 会诊医生
        /// </summary>
        public string Doctor { get; set; }

        /// <summary>
        /// 病历表Id
        /// </summary>
        public int MHId { get; set; }

        /// <summary>
        /// 会诊状态颜色样式
        /// </summary>
        public string ConsultationStatusColorClass { get; set; }

        /// <summary>
        /// 病历会诊状态
        /// </summary>
        public string MHConsultationStatus { get; set; }

        /// <summary>
        /// 病历会诊状态颜色样式
        /// </summary>
        public string MHConsultationStatusColorClass { get; set; }

        /// <summary>
        /// 是否为最新的会诊
        /// </summary>
        public bool IsNewest { get; set; }

    }
}
