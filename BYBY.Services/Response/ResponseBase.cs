namespace BYBY.Services.Response
{
    public abstract class ResponseBase
    {
        public bool Result { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

 
    }
}
