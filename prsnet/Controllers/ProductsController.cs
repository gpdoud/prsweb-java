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
    public class ProductsController : ApiController {

        private PrsDbContext db = new PrsDbContext();

        [HttpGet]
        [ActionName("List")]
        public JsonResponse GetProducts() {
            return JsonResponse.Ok(db.Products.ToList());
        }

        [HttpGet]
        [ActionName("Get")]
        public JsonResponse GetProduct(int? id) {
            if(id == null)
                return JsonResponse.IdNotFound("Product", id);

            var product = db.Products.Find(id);

            if(product == null)
                return JsonResponse.IdNotFound("Product", id);

            return JsonResponse.Ok(product);
        }

        [HttpPost]
        [ActionName("Create")]
        public JsonResponse Create([FromBody] Product product) {
            if(product == null)
                return JsonResponse.EntityIsNull("Product");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            db.Products.Add(product);
            db.SaveChanges();

            return JsonResponse.Ok(product);
        }

        [HttpPost]
        [ActionName("Change")]
        public JsonResponse Change([FromBody] Product product) {
            if(product == null)
                return JsonResponse.EntityIsNull("Product");

            if(!ModelState.IsValid)
                return JsonResponse.ModelStateError(ModelState.Values);

            product.Vendor = null;
            product.DateUpdated = DateTime.Now;
            db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return JsonResponse.Ok(product);
        }

        [HttpPost]
        [ActionName("Remove")]
        public JsonResponse Remove([FromBody] Product product) {
            if(product == null)
                return JsonResponse.EntityIsNull("Product");

            db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return JsonResponse.Ok(product);
        }
    }
}
