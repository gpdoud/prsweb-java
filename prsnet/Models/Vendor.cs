using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prsnet.Models {

    public class Vendor {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        [Index("IDX_Vendor_Code", IsUnique = true)]
        public string Code { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(2)]
        public string State { get; set; }

        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public bool IsPreapproved { get; set; } = false;

        public bool Active { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = null;

        public int? UpdatedBy { get; set; } = null;
    }
}