namespace BYBY.Services.Response
{
    public class LoginResponse : ResponseBase
    {

    }

    public class EmptyResponse : ResponseBase
    {
        public static EmptyResponse CreateSuccess(string successMessage = null)
        {
            var info = new EmptyResponse();
            info.Result = true;
            if (successMessage != null)
            {
                info.SuccessMessage = successMessage;
            }
            return info;
        }

        public static EmptyResponse CreateError(string message)
        {
            var info = new EmptyResponse();
            info.Result = false;
            info.ErrorMessage = message;
            return info;
        }
    }


    //public class SuccessEmptyResponse : ResponseBase
    //{
    //    public static SuccessEmptyResponse Create()
    //    {
    //        var info = new SuccessEmptyResponse();
    //        info.Result = true;
    //        return info;
    //    }
    //}
}
