using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Infrastructure
{
    public enum RoleType
    {
        patient = 1,
        doctor
    }



    public enum CardType
    {
        [Description("身份证")]
        SFZ,
        [Description("居住证")]
        JuZhuZheng,
        [Description("签证")]
        QianZheng,
        [Description("护照")]
        HuZhao,
        [Description("户口本")]
        HuKouBen,
        [Description("军人证")]
        JunRenZheng
    }

    /// <summary>
    /// 婚姻状态
    /// </summary>
    public enum MaritalStatus
    {
        [Description("未婚")]
        WeiHun = 1,
        [Description("已婚")]
        JieHun,
        [Description("离婚")]
        LiHun,
        [Description("再婚")]
        ZaiHun
    }

    public enum SortType
    {
        None,
        Normal,
        Asc,
        Desc
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        Male = 1,
        Female = 0
    }

    /// <summary>
    /// 会诊状态
    /// </summary>
    public enum ConsultationStatus
    {
        [Description("无")]
        No,
        [Description("已取消")]
        Cancel,
        [Description("已完成")]
        Complete
    }

    /// <summary>
    /// 转诊状态
    /// </summary>
    public enum ReferralStatus
    {
        [Description("未申请")]
        NotRequest,
        [Description("申请中")]
        Requesting,
        [Description("已确认")]
        Confirm
    }


}
