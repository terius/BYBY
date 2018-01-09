using BYBY.Infrastructure;

namespace BYBY.Services.Views
{
    public class ReferralListView
    {
        public int Id { get; set; }
        [View("女方姓名")]
        public string FemaleName { get; set; }
        [View("女方年龄")]
        public int FemaleAge { get; set; }
        [View("男方姓名")]
        public string MaleName { get; set; }
        [View("男方年龄")]
        public int MaleAge { get; set; }


        [View("转诊状态")]
        public string ReferralStatus { get; set; }


        [View("添加时间")]
        public string AddTime { get; set; }
        [View("申请用户")]
        public string AddUser { get; set; }
        [View("转诊医院")]
        public string Hospital { get; set; }
        [View("请求时间")]
        public string RequestDate { get; set; }
        [View("转诊医生")]
        public string Doctor { get; set; }

        public int MHId { get; set; }
        [View("备注")]
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
