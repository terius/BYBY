using BYBY.Infrastructure;
using BYBY.Services.Views;
using System.Collections.Generic;

namespace BYBY.Services.Models
{
    public class ConsultationRoomModel
    {
        public IList<SelectItem> HospitalList { get; set; }
        public IList<ConsultationRoomListView> RoomList { get; set; }
    }
}
