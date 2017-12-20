﻿using System.ComponentModel;

namespace BYBY.Infrastructure
{
    public enum RoleType
    {
        patient = 1,
        doctor,
        customerservice
    }



    public enum CardType
    {
        [Description("身份证")]
        SFZ = 1,
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
        YiHun,
        [Description("离婚")]
        LiHun,
        [Description("再婚")]
        ZaiHun
    }

    public enum SortType
    {
        None = 1,
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
        Female = 2
    }

    /// <summary>
    /// 会诊状态
    /// </summary>
    public enum ConsultationStatus
    {
        [Description("无")]
        No = 1,
        [Description("申请中")]
        Requesting = 2,
        [Description("已取消")]
        Cancel = 3,
        [Description("已确认")]
        Confirm = 4,
        [Description("已完成")]
        Complete = 5,
    }

    /// <summary>
    /// 转诊状态
    /// </summary>
    public enum ReferralStatus
    {
        [Description("无")]
        No = 1,
        [Description("申请中")]
        Requesting = 2,
        [Description("已取消")]
        Cancel = 3,
        [Description("已确认")]
        Confirm = 4
    }


    public enum Education
    {
        [Description("小学")]
        XiaoXue = 1,
        [Description("初中")]
        ChuZhong,
        [Description("高中")]
        GaoZhong,
        [Description("中专")]
        ZhongZhuan,
        [Description("大专")]
        DaZhuan,
        [Description("本科")]
        BenKe,
        [Description("硕士")]
        SuoShi,
        [Description("博士")]
        BoShi
    }

    public enum DBAction
    {
        Add,
        Update,
        Delete
    }


    public enum ErrorType
    {
        NotEmpty,
        DateTimeError,
        NumberError,
        DupError
    }

}
