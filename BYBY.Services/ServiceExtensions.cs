using BYBY.Services.Response;

namespace BYBY.Services
{
    public static class ServiceExtensions
    {
        public static void CreateErrorResponse(this ResponseBase response, string errorMessage)
        {
            response.Result = false;
            response.ErrorMessage = errorMessage;
        }

        public static void CreateSuccessResponse(this ResponseBase response, string successMessage = null)
        {
            response.Result = true;
            response.SuccessMessage = successMessage;
        }
    }
}
