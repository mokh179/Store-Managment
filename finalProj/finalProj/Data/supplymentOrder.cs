namespace finalProj
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("supplymentOrder")]
    public partial class supplymentOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public supplymentOrder()
        {
            supplyment_orderDetails = new HashSet<supplyment_orderDetails>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderID { get; set; }

        public int supplierID { get; set; }

        public int stockID { get; set; }

        [Column(TypeName = "date")]
        public DateTime orderDate { get; set; }

        public virtual stock stock { get; set; }

        public virtual supplier supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplyment_orderDetails> supplyment_orderDetails { get; set; }
    }
}
