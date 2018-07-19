using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Models
{
    public class Customer : Entity
    {
        [BsonElement("username")]
        public string UserName { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("pswd_hash")]
        public byte[] PasswordHash { get; set; }

        [BsonElement("pswd_salt")]
        public byte[] PasswordSalt { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phone")]
        public int Phone { get; set; }

        [BsonElement("photo")]
        public string Photo { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        

    }
}
