namespace MessagingService.Core.Entities.Base
{
    [System.Serializable]
    public class MessagingServiceApiException : System.Exception
    {
        public MessagingServiceApiException() { }
        public MessagingServiceApiException(string message) : base(message) { }
        public MessagingServiceApiException(string message, System.Exception inner) : base(message, inner) { }
        protected MessagingServiceApiException(
         System.Runtime.Serialization.SerializationInfo info,
         System.Runtime.Serialization.StreamingContext context) : base(info, context) { }


    }
}
