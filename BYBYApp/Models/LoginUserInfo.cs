using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BYBYApp.Models
{
    public class LoginUserInfo
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public string Name { get; set; }

        public RoleType RoleName { get; set; }


        public bool IsMasterDoctor { get; set; }

    }
}