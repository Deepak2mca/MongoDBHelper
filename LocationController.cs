using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GeoCoordinatePortable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    //[Route("api/[controller]")]
    public class LocationController : Controller
    {

        [HttpGet]
        [Route("api/Location/GetAll")]
        public string GetAll()
        {           
            List<CarPark> aList = MongoDBHelper.GetEntityList<CarPark>();
            var json = JsonConvert.SerializeObject(aList);
            return json;
        }
        [HttpGet]
        [Route("api/Location/GetByLocationName")]
        public string GetByLocationName(string locname)
        {
            
            var filter = Builders<CarPark>.Filter.Regex("name", new BsonRegularExpression(locname, "i"));
            Task<string> x  = MongoDBHelper.FindCollectionByFilterAsync<CarPark>(filter);
            x.Wait();
            return x.Result;
        }
        [HttpGet]        
        [Route("api/Location/GetByLatLong")]
        public string GetByLatLong(Double lat,Double lng)
        {
            List<CarPark> location = MongoDBHelper.GetCollection<CarPark>()
                .FindAllAs<CarPark>().Where<CarPark>(sb => sb.name.ToLower().Contains("mahape")).ToList<CarPark>();
            // .Where<CarPark>(sb => sb.loc.coordinates[0] == lat && sb.loc.coordinates[1] == lng).ToList<CarPark>();
            var json = JsonConvert.SerializeObject(location);
            return json;

            //var coord = new GeoCoordinate(lat, lng);
            //var nearest = MongoDBHelper.GetCollection<CarPark>().FindAll().Select(x => new GeoCoordinate(x.loc.coordinates[0], x.loc.coordinates[1]))
            //.OrderBy(x => x.GetDistanceTo(coord));

        }
        [HttpPut]
        public int AddLocation()
        {
            System.Collections.Generic.List<double> items2 = new System.Collections.Generic.List<double> { 1.23222, 4.56222 };
            CarPark documnt = new CarPark
            {
                _id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                name = "Parking Spaces,Dadar",
                tspaces = 200,
                aspaces = 70,
                loc = new Loc() { type = "point", coordinates = items2 }
            };
            MongoDBHelper.InsertEntity<CarPark>(documnt);
            return 1;
        }
    }
}
