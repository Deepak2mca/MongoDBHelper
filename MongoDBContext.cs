using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace ParkingService.Models
{    
    public class MongoDBContext : DbContext
    {
        public static string ConnectionString = "mongodb+srv://admin:admin@cluster0-hfxr5.mongodb.net/test"; //{ get; set; }
        public static string DatabaseName = "carparkdb";// { get; set; }
        public static bool IsSSL = false;// { get; set; }

        private IMongoDatabase _database { get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }

        public MongoDBContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to db server.", ex);
            }
        }

        public IMongoCollection<CarPark> ParkLocations
        {
            get
            {
                return _database.GetCollection<CarPark>("car_park");
            }
        }
    }
}
