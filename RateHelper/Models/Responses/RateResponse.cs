namespace RateHelper.Models.Responses
{
    public class RateResponse : BaseResponse
    {
        private RateResponse(bool success, string message, Rate rate)
            : base(success, message)
        {
            Rate = rate;
        }

        public RateResponse(Rate rate) :
            this(true, string.Empty, rate)
        {
        }

        public RateResponse(string message) :
            this(false, message, null)
        {
        }

        public Rate Rate { get; }
    }
}