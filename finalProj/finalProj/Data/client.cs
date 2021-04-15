namespace finalProj
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("client")]
    public partial class client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public client()
        {
            clientOrders = new HashSet<clientOrder>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int clientID { get; set; }

        [Required]
        [StringLength(100)]
        public string clientName { get; set; }

        [Required]
        [StringLength(11)]
        public string clientPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string clientFax { get; set; }

        [Required]
        [StringLength(50)]
        public string clientMail { get; set; }

        [Required]
        [StringLength(50)]
        public string clientSite { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<clientOrder> clientOrders { get; set; }
    }
}
