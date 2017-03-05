namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string NameCompany { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        public int Phone { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(1000)]
        public string AddressWeb { get; set; }

        [StringLength(300)]
        public string AddressSocial { get; set; }
    }
}
