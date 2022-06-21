namespace MessagingService.Core.Entities.Base
{
    public class BaseMessagingServiceResponse
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public object Body { get; set; }
    }
}
