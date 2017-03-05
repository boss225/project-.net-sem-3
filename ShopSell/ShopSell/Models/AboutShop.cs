namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;
    [Table("AboutShop")]
    public partial class AboutShop
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Title { get; set; }

        [AllowHtml]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
    }
}
