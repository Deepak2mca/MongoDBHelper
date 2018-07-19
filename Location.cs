using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ParkingService.Models
{

    public abstract class Entity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]        
        public string _id { set; get; }
    }

    public class Loc
    {
        [BsonElement("type")]
        public string type { get; set; }
        [BsonElement("coordinates")]
        public List<double> coordinates { get; set; }
    }

    public class CarPark : Entity
    {
       
        [BsonElement("name")]
        public string name { get; set; }
        [BsonElement("tspaces")]
        public int tspaces { get; set; }
        [BsonElement("aspaces")]
        public int aspaces { get; set; }
        [BsonElement("loc")]
        public Loc loc { get; set; }       

    }
}
