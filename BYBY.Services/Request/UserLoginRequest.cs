﻿using BYBY.Infrastructure;

namespace BYBY.Services.Request
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string RoleId { get; set; }
    }
}
