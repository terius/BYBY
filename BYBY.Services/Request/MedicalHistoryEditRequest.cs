namespace BYBY.Services.Request
{
    public class MedicalHistoryEditRequest : MedicalHistoryAddRequest
    {
        public int Id { get; set; }

        public int FemaleId { get; set; }
        public int MaleId { get; set; }


        public string FemaleCardTypeText { get; set; }
        public string FemaleMarriageText { get; set; }

        public string FemaleNationText { get; set; }
        public string FemaleJobText { get; set; }
        public string FemaleEthnicText { get; set; }

        public string FemaleEducationText { get; set; }


        public string MaleCardTypeText { get; set; }
        public string MaleMarriageText { get; set; }

        public string MaleNationText { get; set; }
        public string MaleJobText { get; set; }
        public string MaleEthnicText { get; set; }

        public string MaleEducationText { get; set; }
    }
}
