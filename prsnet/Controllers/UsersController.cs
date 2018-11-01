using prsnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace prsnet.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController {

        private PrsDbContext db = new PrsDbContext();

        [HttpGet]
        [ActionName("Login")]
        public JsonResponse DoLogin(string username, string password) {
            if(username == null || password == null)
                return JsonResponse.LoginFailed();

            var user = db.Users.SingleOrDefault(
                u => u.Username.ToUpper().Equals(username.ToUpper()) 
                    && u.Password.Equals(password)
            );

            if(user == null)
                return JsonResponse.LoginFailed();

            return JsonResponse.Ok(user);
        }

        [HttpGet]
        [ActionName("List")]
        public JsonResponse GetUsers() {
            return JsonResponse.Ok(db.Users.ToList());
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResponse GetUser(int? id) {
            if(id == null)
                return JsonResponse.IdNotFound("User", id);

            var user = db.Users.Find(id);

            if(user == null)
                return JsonResponse.IdNotFound("User", id);

            return JsonResponse.Ok(user);
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create([FromBody] User user) {
            if(user == null)
                return JsonResponse.EntityIsNull("User");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            db.Users.Add(user);
            db.SaveChanges();

            return JsonResponse.Ok(user);
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change([FromBody] User user) {
            if(user == null)
                return JsonResponse.EntityIsNull("User");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            user.DateUpdated = DateTime.Now;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return JsonResponse.Ok(user);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove([FromBody] User user) {
            if(user == null)
                return JsonResponse.EntityIsNull("User");

            db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return JsonResponse.Ok(user);
        }

    }
}
