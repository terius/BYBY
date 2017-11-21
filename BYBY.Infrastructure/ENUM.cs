using System;
using System.Collections.Generic;
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
        SFZ
    }

    /// <summary>
    /// 婚姻状态
    /// </summary>
    public enum MaritalStatus
    {
        WeiHun = 1,
        JieHun,
        LiHun
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
}
