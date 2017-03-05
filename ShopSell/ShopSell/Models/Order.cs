namespace ShopSell.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [StringLength(50)]
        public string Code { get; set; }

        [UIHint("UTCTime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactName { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactAddress { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactPhone { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactEmail { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactReceiverName { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactReceiverAddress { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactReceiverPhone { get; set; }

        [Required]
        [StringLength(250)]
        public string ContactReceiverEmail { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        public decimal TotalPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
