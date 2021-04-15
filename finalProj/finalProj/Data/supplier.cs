namespace finalProj
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplier")]
    public partial class supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplier()
        {
            client_orderDetails = new HashSet<client_orderDetails>();
            movements = new HashSet<movement>();
            supplymentOrders = new HashSet<supplymentOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int supplierID { get; set; }

        [Required]
        [StringLength(100)]
        public string supplierName { get; set; }

        [Required]
        [StringLength(11)]
        public string supplierPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string supplierFax { get; set; }

        [Required]
        [StringLength(50)]
        public string supplierMail { get; set; }

        [Required]
        [StringLength(50)]
        public string supplierSite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<client_orderDetails> client_orderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<movement> movements { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplymentOrder> supplymentOrders { get; set; }
    }
}
