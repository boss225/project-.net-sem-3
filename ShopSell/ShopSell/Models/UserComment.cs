namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    public partial class UserComment
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}
