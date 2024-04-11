using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Copernicus2.Models
{
    public partial class CustomersContext : DbContext
    {
        public CustomersContext()
        {
        }

        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\MSSQLSERVER01;Database=Customers;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Company)
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.Country)
                    .HasMaxLength(51)
                    .HasColumnName("country");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(50)
                    .HasColumnName("created_at");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.First)
                    .HasMaxLength(50)
                    .HasColumnName("first");

                entity.Property(e => e.Last)
                    .HasMaxLength(50)
                    .HasColumnName("last");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
