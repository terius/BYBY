namespace BYBY.Services.Request
{
    public class MedicalHistoryEditRequest : MedicalHistoryAddRequest
    {
        public int Id { get; set; }

        public int FemaleId { get; set; }
        public int MaleId { get; set; }
    }
}
