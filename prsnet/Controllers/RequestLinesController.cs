using prsnet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace prsnet.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RequestLinesController : ApiController
    {
        private PrsDbContext db = new PrsDbContext();

        private void UpdateRequestTotal(int id) {
            var request = db.Requests.Find(id);
            var total = db.RequestLines
                .Where(rl => rl.RequestId == id)
                .Sum(rli => rli.Product.Price * rli.Quantity);
            request.Total = total;
            db.Entry(request).State = EntityState.Modified;
            db.SaveChanges();
        }

        [HttpGet]
        [ActionName("List")]
        public JsonResponse GetRequestLines() {
            return JsonResponse.Ok(db.RequestLines.ToList());
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResponse GetRequestLine(int? id) {
            if(id == null)
                return JsonResponse.IdNotFound("RequestLine", id);

            var requestLine = db.RequestLines.Find(id);

            if(requestLine == null)
                return JsonResponse.IdNotFound("RequestLine", id);

            return JsonResponse.Ok(requestLine);
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create([FromBody] RequestLine requestLine) {
            if(requestLine == null)
                return JsonResponse.EntityIsNull("RequestLine");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            db.RequestLines.Add(requestLine);
            db.SaveChanges();

            UpdateRequestTotal(requestLine.RequestId);

            return JsonResponse.Ok(requestLine);
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change([FromBody] RequestLine requestLine) {
            if(requestLine == null)
                return JsonResponse.EntityIsNull("RequestLine");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            requestLine.Product = null;
            requestLine.Request = null;
            requestLine.DateUpdated = DateTime.Now;
            db.Entry(requestLine).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            UpdateRequestTotal(requestLine.RequestId);

            return JsonResponse.Ok(requestLine);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove([FromBody] RequestLine requestLine) {
            if(requestLine == null)
                return JsonResponse.EntityIsNull("RequestLine");

            db.Entry(requestLine).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            UpdateRequestTotal(requestLine.RequestId);

            return JsonResponse.Ok(requestLine);
        }
    }
}
