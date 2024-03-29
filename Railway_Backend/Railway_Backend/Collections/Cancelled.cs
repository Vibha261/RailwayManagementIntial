﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Railway_Backend.Collections
{
    public class Cancelled
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("trainName")]
        public string TrainName { get; set; }

        [BsonElement("trainNumber")]
        public string TrainNumber { get; set;}

        [BsonElement("from")]
        public string From { get; set;}

        [BsonElement("to")]
        public string To { get; set;}

        [BsonElement("date")]
        public string Date { get; set;}
    }
}
