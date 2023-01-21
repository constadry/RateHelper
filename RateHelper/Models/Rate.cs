using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RateHelper.Models
{
    public class Rate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string RateName { get; set; }
        public double Value { get; set; }
        [BsonDateTimeOptions]
        public DateTime UpdateDate { get; set; }
    }
}