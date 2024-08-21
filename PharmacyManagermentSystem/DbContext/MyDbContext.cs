using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.Model;

namespace PharmacyManagermentSystem.DbContext
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<DestructiveMedicine> DestructiveMedicines { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<ImageCategogy> ImageCategogies { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescribeMedicine> PrescribeMedicines { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Receipt> Receipts { get; set; }
        public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual DbSet<ReturnSupplier> ReturnSuppliers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Salary> Salarys { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<UserShift> UserShifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(u => u.Orders) // Specify how the User navigates back to Orders
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: prevents cascading delete

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Employee)
                .WithMany() // Employee relationship does not need to map back to Orders
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: prevents cascading delete

            // Configure other entities and relationships as needed.
        }



    }
}
