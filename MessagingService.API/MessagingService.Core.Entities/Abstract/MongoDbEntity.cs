using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MessagingService.Core.Entities.Abstract
{
    public abstract class MongoDbEntity : IEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
