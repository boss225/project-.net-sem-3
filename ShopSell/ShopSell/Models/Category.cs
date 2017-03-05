namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int ParentLevel { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public int? SortOrder { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}
