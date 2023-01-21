namespace RateHelper.Models.Responses
{
    public class BaseResponse
    {
        public bool Success { get; }
        public string Message { get; }

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}