using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace finalProj
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model110")
        {
        }

        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<client_orderDetails> client_orderDetails { get; set; }
        public virtual DbSet<clientOrder> clientOrders { get; set; }
        public virtual DbSet<item> items { get; set; }
        public virtual DbSet<manager> managers { get; set; }
        public virtual DbSet<movement> movements { get; set; }
        public virtual DbSet<stock> stocks { get; set; }
        public virtual DbSet<stock_item> stock_item { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<supplyment_orderDetails> supplyment_orderDetails { get; set; }
        public virtual DbSet<supplymentOrder> supplymentOrders { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<item>()
                .HasMany(e => e.client_orderDetails)
                .WithRequired(e => e.item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<item>()
                .HasMany(e => e.movements)
                .WithRequired(e => e.item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<item>()
                .HasMany(e => e.supplyment_orderDetails)
                .WithRequired(e => e.item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<manager>()
                .HasMany(e => e.stocks)
                .WithOptional(e => e.manager)
                .HasForeignKey(e => e.manegerID);

            modelBuilder.Entity<stock>()
                .HasMany(e => e.clientOrders)
                .WithRequired(e => e.stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<stock>()
                .HasMany(e => e.movements)
                .WithRequired(e => e.stock)
                .HasForeignKey(e => e.fromStock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<stock>()
                .HasMany(e => e.movements1)
                .WithRequired(e => e.stock1)
                .HasForeignKey(e => e.toStock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplier>()
                .HasMany(e => e.client_orderDetails)
                .WithRequired(e => e.supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<supplier>()
                .HasMany(e => e.supplymentOrders)
                .WithRequired(e => e.supplier)
                .WillCascadeOnDelete(false);
        }
    }
}
