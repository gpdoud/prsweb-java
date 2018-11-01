using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace prsnet.Models {
    public class User {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Index("IDX_Username", IsUnique = true)]
        public string Username { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        [StringLength(30)]
        public string Firstname { get; set; }

        [StringLength(30)]
        public string Lastname { get; set; }

        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public bool IsReviewer { get; set; } = false;

        public bool IsAdmin { get; set; } = false;

        public bool Active { get; set; } = true;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = null;

        public int? UpdatedBy { get; set; } = null;

        public User() { }

    }
}