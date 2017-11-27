using BYBY.Infrastructure;
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
        public CardType? FemaleCardType { get; set; }
        public CardType? MaleCardType { get; set; }

    }
}
