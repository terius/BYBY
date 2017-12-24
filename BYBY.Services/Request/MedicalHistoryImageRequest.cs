namespace BYBY.Services.Request
{
    public class MedicalHistoryImageRequest
    {
        public int Id { get; set; }

        public string FilePath { get; set; }

        public string Name { get; set; }

        public int TBMedicalHistoryId { get; set; }

    }
}
