using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prsnet.Models {
    public class Request {

        public readonly static string NEW = "NEW";
        public readonly static string REVIEW = "REVIEW";
        public readonly static string APPROVED = "APPROVED";
        public readonly static string REJECTED = "REJECTED";

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Justification { get; set; }

        [StringLength(255)]
        public string RejectionReason { get; set; }

        [StringLength(30)]
        public string DeliveryMode { get; set; } = "Pickup";

        public DateTime? SubmittedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(30)]
        public string Status { get; set; } = NEW;

        public decimal Total { get; set; } = 0;

        public bool Active { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = null;

        public int? UpdatedBy { get; set; } = null;
    }
}