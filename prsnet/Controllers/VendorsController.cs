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
    public class VendorsController : ApiController {

        private PrsDbContext db = new PrsDbContext();

        [HttpGet]
        [ActionName("List")]
        public JsonResponse GetVendors() {
            return JsonResponse.Ok(db.Vendors.ToList());
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResponse GetVendor(int? id) {
            if(id == null)
                return JsonResponse.IdNotFound("Vendor", id);

            var vendor = db.Vendors.Find(id);

            if(vendor == null)
                return JsonResponse.IdNotFound("Vendor", id);

            return JsonResponse.Ok(vendor);
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create([FromBody] Vendor vendor) {
            if(vendor == null)
                return JsonResponse.EntityIsNull("Vendor");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            db.Vendors.Add(vendor);
            db.SaveChanges();

            return JsonResponse.Ok(vendor);
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change([FromBody] Vendor vendor) {
            if(vendor == null)
                return JsonResponse.EntityIsNull("Vendor");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            vendor.DateUpdated = DateTime.Now;
            db.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return JsonResponse.Ok(vendor);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove([FromBody] Vendor vendor) {
            if(vendor == null)
                return JsonResponse.EntityIsNull("Vendor");

            db.Entry(vendor).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return JsonResponse.Ok(vendor);
        }

    }
}
