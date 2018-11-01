using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prsnet.Models {

    public class Product {

        [Key]
        public int Id { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }

        [StringLength(30)]
        public string PartNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public decimal Price { get; set; } = 0;

        [StringLength(10)]
        public string Unit { get; set; } = "Each";

        [StringLength(255)]
        public string PhotoPath { get; set; }

        public bool Active { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = null;

        public int? UpdatedBy { get; set; } = null;

    }
}