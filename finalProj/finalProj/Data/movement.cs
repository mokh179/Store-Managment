namespace finalProj
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class movement
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime moveDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int itemCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int fromStock { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int toStock { get; set; }

        public int? SupplierID { get; set; }

        public int Quantity { get; set; }

        public int expirationTime { get; set; }

        public virtual item item { get; set; }

        public virtual stock stock { get; set; }

        public virtual stock stock1 { get; set; }

        public virtual supplier supplier { get; set; }
    }
}
