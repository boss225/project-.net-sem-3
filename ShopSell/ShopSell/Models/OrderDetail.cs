namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }

        public int ProductId { get; set; }

        [Required]
        [StringLength(250)]
        public string ProductName { get; set; }

        [StringLength(1000)]
        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal Price { get; set; }

        public int Discount { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal ReducedPrice { get; set; }

        public virtual Order Order { get; set; }
    }
}
