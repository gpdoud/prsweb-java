using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prsnet.Models {
    public class RequestLine {

        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        public int Quantity { get; set; } = 1;

        public bool Active { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = null;

        public int? UpdatedBy { get; set; } = null;
    }
}