using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using ParkingService.Common;
using ParkingService.Models;

namespace ParkingService.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        [HttpGet(Name = "Authenticate")]        
        public string Authenticate(string username, string password)
        {
            AuthResponse objResponse = new AuthResponse();
            objResponse.Status = 2;
            try
            {                
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var query = Query.And(Query.EQ("username", username), Query.EQ("password", password));
                    var objCustomer = MongoDBHelper.SearchByQueryObject<Customer>(query, "Customer");
                    objResponse.Status = objCustomer != null ? 0 : 2;
                    objResponse.classobject = objCustomer;
                }
            }
            catch (System.Exception)
            {
                objResponse.Status=1;
            }            
            return JsonConvert.SerializeObject(objResponse);
        }

        [HttpPost(Name = "Add")]       
        public string AddUser([FromBody]Customer objCustomer)
        {
            AuthResponse objResponse = new AuthResponse();
           // byte[] passwordHash, passwordSalt;
            //Encryption.CreatePasswordHash("c@rpark", out passwordHash, out passwordSalt);  

            //objUser.PasswordHash = passwordHash;
           // objUser.PasswordSalt = passwordSalt;
            try
            {
                var query = Query.And(Query.EQ("username", objCustomer.UserName));
                var objSearchCustomer = MongoDBHelper.SearchByQueryObject<Customer>(query, "Customer");
                if (objSearchCustomer != null)
                {
                    objResponse.classobject = objSearchCustomer;
                    objResponse.Status = 2;
                }
                else
                {
                    MongoDBHelper.InsertEntity<Customer>(objCustomer);
                    objResponse.classobject = objCustomer;
                    objResponse.Status = 0;
                }
            }
            catch (System.Exception ex)
            {
                objResponse.Status = 1;
                objResponse.classobject = ex.Message;
            }          
            return JsonConvert.SerializeObject(objResponse);
        }


        //[HttpPut(Name = "Update")]
        //public string UpdateUser(User objUser)
        //{
        //    // validation
        //    if (string.IsNullOrWhiteSpace(password))
        //        return "Password is required";

        //    //if (false)
        //    //   return ("Username " + user.Username + " is already taken");

        //    byte[] passwordHash, passwordSalt;
        //    Encryption.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    User objUser = new Models.User { FirstName= firstName ,LastName= lastName ,Username= userName };
        //    objUser.PasswordHash = passwordHash;
        //    objUser.PasswordSalt = passwordSalt;
        //    MongoDBHelper.InsertEntity<User>(objUser);

        //    return "Added Successfully";
        //}

        //[Http(Name = "GetUser")]
        //public string GetUser(User objUser)
        //{
        //    // validation
        //    if (string.IsNullOrWhiteSpace(password))
        //        return "Password is required";

        //    //if (false)
        //    //   return ("Username " + user.Username + " is already taken");

        //    byte[] passwordHash, passwordSalt;
        //    Encryption.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    User objUser = new Models.User { FirstName = firstName, LastName = lastName, Username = userName };
        //    objUser.PasswordHash = passwordHash;
        //    objUser.PasswordSalt = passwordSalt;
        //    MongoDBHelper.InsertEntity<User>(objUser);

        //    return "Added Successfully";
        //}


    }
}