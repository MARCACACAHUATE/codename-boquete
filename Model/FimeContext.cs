using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace codename_boquete.Model
{
    public partial class FimeContext : DbContext
    {
        public FimeContext()
        {
        }

        public FimeContext(DbContextOptions<FimeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CsrfDataNumber> CsrfDataNumbers { get; set; } = null!;
        public virtual DbSet<CsrfDato> CsrfDatos { get; set; } = null!;
        public virtual DbSet<CsrfReporteDeFuga> CsrfReporteDeFugas { get; set; } = null!;
        public virtual DbSet<CsrfRetrabajadore> CsrfRetrabajadores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string startupPath = Environment.CurrentDirectory;
                optionsBuilder.UseSqlite($"DataSource={startupPath}\\Fime.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsrfDataNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_DataNumber");
            });

            modelBuilder.Entity<CsrfDato>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_Datos");

                entity.Property(e => e.ProveedorTxv).HasColumnName("ProveedorTXV");
            });

            modelBuilder.Entity<CsrfReporteDeFuga>(entity =>
            {
                entity.ToTable("CSRF_ReporteDeFugas");

                entity.Property(e => e.CostXunit).HasColumnName("CostXUnit");
            });

            modelBuilder.Entity<CsrfRetrabajadore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_Retrabajadores");

                entity.Property(e => e.Induccion).HasColumnType("varchar");

                entity.Property(e => e.Nombre).HasColumnType("varchar");

                entity.Property(e => e.Nomina).HasColumnType("varchar");

                entity.Property(e => e.Puesto).HasColumnType("varchar");

                entity.Property(e => e.ReparacionAb)
                    .HasColumnType("varchar")
                    .HasColumnName("ReparacionAB");

                entity.Property(e => e.ReparacionCamara).HasColumnType("varchar");

                entity.Property(e => e.SoldaduraManual).HasColumnType("varchar");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
