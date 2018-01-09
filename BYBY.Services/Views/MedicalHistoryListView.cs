using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BYBY.Services.Views
{
    public class MedicalHistoryListView
    {
        public int Id { get; set; }
        public string FeMaleName { get; set; }
        public int FeMaleAge { get; set; }
        public string MaleName { get; set; }
        public int MaleAge { get; set; }

        public string FeMalePhone { get; set; }
        public string MalePhone { get; set; }

        public string ConsultationStatus { get; set; }

        public string ReferralStatus { get; set; }

        public string Addtime { get; set; }


        public string FeMaleBirthday { get; set; }
        public string FeMaleMarrad { get; set; }
        public string FeMaleFixPhone { get; set; }
      


        public string MaleBirthday { get; set; }
        public string MaleMarrad { get; set; }
        public string MaleFixPhone { get; set; }
        public string AddUserName { get; set; }

        public int NewestConsultationId { get; set; }
        public int NewestReferralId { get; set; }
    }
}
