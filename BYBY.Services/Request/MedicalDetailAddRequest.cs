using System.ComponentModel.DataAnnotations;

namespace BYBY.Services.Request
{
    public class MedicalDetailAddRequest :MedicalDetailRequest
    {
        public int PatientId { get; set; }


    }
}
