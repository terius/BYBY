namespace BYBY.Services.Views
{
    public class ReferralListView
    {
        public int Id { get; set; }
        public string FemaleName { get; set; }
        public int FemaleAge { get; set; }
        public string MaleName { get; set; }
        public int MaleAge { get; set; }

      

        public string ReferralStatus { get; set; }

      

        public string AddTime { get; set; }
        public string AddUser { get; set; }

        public string Hospital { get; set; }

        public string RequestDate { get; set; }

        public string Doctor { get; set; }

        public int MHId { get; set; }

        public string Remark { get; set; }
        /// <summary>
        /// 会诊状态颜色样式
        /// </summary>
        public string ReferralStatusColorClass { get; set; }

        /// <summary>
        /// 病历转诊状态
        /// </summary>
        public string MHReferralStatus { get; set; }

        /// <summary>
        /// 病历转诊状态颜色样式
        /// </summary>
        public string MHReferralStatusColorClass { get; set; }

        /// <summary>
        /// 是否为最新的转诊
        /// </summary>
        public bool IsNewest { get; set; }

    }
}
