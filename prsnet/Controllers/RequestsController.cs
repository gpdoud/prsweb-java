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
    public class RequestsController : ApiController {

        private PrsDbContext db = new PrsDbContext();

        [HttpGet]
        [ActionName("Review")]
        public JsonResponse Review(int? prid) {
            if(prid == null)
                return JsonResponse.IdNotFound("Request", prid);
            var request = db.Requests.Find(prid);
            request.Status = (request.Total <= 50)
                ? prsnet.Models.Request.APPROVED
                : prsnet.Models.Request.REVIEW;
            db.SaveChanges();
            return JsonResponse.Ok(request);
        }

        [HttpGet]
        [ActionName("Reviewlist")]
        public JsonResponse GetRequestsForReview(int? userid) {
            var requests = db.Requests.Where(r => r.Status == "REVIEW").ToList();
            if(userid != null) {
                requests = requests.Where(u => u.UserId != userid).ToList();
            }
            return JsonResponse.Ok(requests);
        }

        [HttpGet]
        [ActionName("List")]
        public JsonResponse GetRequests() {
            return JsonResponse.Ok(db.Requests.ToList());
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResponse GetRequest(int? id) {
            if(id == null)
                return JsonResponse.IdNotFound("Request", id);

            var request = db.Requests.Find(id);

            if(request == null)
                return JsonResponse.IdNotFound("Request", id);

            return JsonResponse.Ok(request);
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create([FromBody] Request request) {
            if(request == null)
                return JsonResponse.EntityIsNull("Request");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            db.Requests.Add(request);
            db.SaveChanges();

            return JsonResponse.Ok(request);
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change([FromBody] Request request) {
            if(request == null)
                return JsonResponse.EntityIsNull("Request");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            request.User = null;
            request.DateUpdated = DateTime.Now;
            db.Entry(request).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return JsonResponse.Ok(request);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove([FromBody] Request request) {
            if(request == null)
                return JsonResponse.EntityIsNull("Request");

            db.Entry(request).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return JsonResponse.Ok(request);
        }
    }
}
