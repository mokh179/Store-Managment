namespace finalProj
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class supplyment_orderDetails
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int orderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemCode { get; set; }

        public int itemPrice { get; set; }

        public int quantity { get; set; }

        public virtual item item { get; set; }

        public virtual supplymentOrder supplymentOrder { get; set; }
    }
}
