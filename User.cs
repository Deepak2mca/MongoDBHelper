using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{   
    public class User : Entity
    {
        [BsonElement("FirstName")]
        public string FirstName { get; set; }
        [BsonElement("LastName")]
        public string LastName { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("PasswordHash")]
        public byte[] PasswordHash { get; set; }
        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("MobileNo")]
        public string MobileNo { get; set; }
        [BsonElement("VehicleRegNo")]
        public string VehicleRegNo { get; set; }
        [BsonElement("Image")]
        byte[] Image { get; set; }
    }
}
