using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace prsnet.Models {

    public class PrsDbContext : DbContext {

        public PrsDbContext() : base("name=PrsDb") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestLine> RequestLines { get; set; }
    }
}