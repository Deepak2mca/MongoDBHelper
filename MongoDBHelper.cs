using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using ParkingService.Common;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ParkingService.Models
{
    public static class MongoDBHelper
    {
        
        private static string connectionString = "mongodb+srv://User:password@cluster0-hfxr0.mongodb.net/test";
        public static string DatabaseName { get { return "test"; } }       
        private static MongoDatabase _database;
        public static MongoDatabase DB
        {
            get
            {
                if (_database == null)
                {

                    //var Client = new MongoClient(connectionString);
                    MongoClient client = new MongoClient(connectionString);
                    
                    MongoServer server = client.GetServer();
                    _database = server.GetDatabase(DatabaseName);                   
                    return _database;
                }
                return _database;
            }
        }      

        public static MongoCollection<T> GetCollection<T>() where T : Entity
        {
            return DB.GetCollection<T>(typeof(T).Name.FirstLetterToUpper());
        }
        public static List<T> GetEntityList<T>() where T : Entity
        {           
            var collection = DB.GetCollection<T>(typeof(T).Name.FirstLetterToUpper());            
            return collection.AsQueryable().ToList<T>();
        }
        public static void InsertEntity<T>(T entity) where T : Entity
        {
           // await GetCollection<T>().InsertOneAsync(entity);
           // GetCollection<T>().Insert(entity);
            GetCollection<T>().Save(entity);
        }
        public static T SearchByObjectID<T>(string objectid) where T: Entity
        {
            var query_id = Query.EQ("_id", ObjectId.Parse(objectid));           
            var entity =  DB.GetCollection<T>("CarPark").FindOne(query_id);
            return entity as T;
        }
        public static T SearchByQueryObject<T>(IMongoQuery mongoQuery,string entityName) where T : Entity
        {          
            var entity = DB.GetCollection<T>(entityName).FindOne(mongoQuery);
            return entity as T;
        }

        public async static Task<string> FindCollectionByFilterAsync<T>(FilterDefinition<T> mongoQuery) where T : Entity
        {
            var client = new MongoClient(connectionString);
            IMongoDatabase db = client.GetDatabase(MongoDBHelper.DatabaseName);
            var collection = db.GetCollection<T>(typeof(T).Name.FirstLetterToUpper());
            List<T> lst=  await collection.FindSync(mongoQuery).ToListAsync();
            var json = JsonConvert.SerializeObject(lst);
            return json;
        }
    }
}
