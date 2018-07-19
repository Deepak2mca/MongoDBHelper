using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Produces("application/json")]
    [Route("api/SlotEmpty")]
    public class SlotEmptyController : Controller
    {
        // GET: api/Park
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Park/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Park
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Park/5
        [HttpPut("{id}")]
        public string Put(string id, [FromBody]string value)
        {
            AuthResponse auth = new AuthResponse();
            CarPark location = MongoDBHelper.SearchByObjectID<CarPark>(id);
            if (location != null)
            {
                location.aspaces = location.aspaces + 1;
                MongoDBHelper.InsertEntity<CarPark>(location);
                auth.Status = 0;
                return JsonConvert.SerializeObject(auth);
            }
            auth.Status = 1;
            return JsonConvert.SerializeObject(auth);

        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
