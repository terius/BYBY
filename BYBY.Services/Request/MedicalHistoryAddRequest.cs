﻿using BYBY.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Request
{
    public class MedicalHistoryAddRequest
    {
        public string MedicalHistoryNo { get; set; }

        public CardType? MaleCardType { get; set; }

        public string FemalePhone { get; set; }
        public string MalePhone { get; set; }
        public string FixPhone { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }

        #region 女方信息
        public CardType? FemaleCardType { get; set; }
        public string FemaleName { get; set; }
        public string FemaleBirthday { get; set; }
        public string FemaleCardNo { get; set; }
        public MaritalStatus? FemaleMarriage { get; set; }
        public int FemaleNation { get; set; }
        public int FemaleJob { get; set; }

        public int FemaleEthnic { get; set; }

        public Education FemaleEducation { get; set; }

        public string FemaleNativePlace { get; set; }
        public string FemaleHouseholdAddress { get; set; }
        #endregion




    }
}
