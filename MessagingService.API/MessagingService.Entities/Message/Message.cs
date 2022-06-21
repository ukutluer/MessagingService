using MessagingService.Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace MessagingService.Entities.Message
{
    public class Message: MongoDbEntity
    {
        public string from { get; set; }
        [Required()]
        public string to { get; set; }
        [Required()]
        public string content { get; set; }
    }
}
