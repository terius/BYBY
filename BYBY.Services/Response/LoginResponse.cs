namespace BYBY.Services.Response
{
    public class LoginResponse : ResponseBase
    {
      
    }

    public class EmptyResponse : ResponseBase
    {
       
    }


    public class SuccessEmptyResponse : ResponseBase
    {
        public SuccessEmptyResponse()
        {
            this.Result = true;
        }
    }
}
