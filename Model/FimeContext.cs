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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlite("DataSource=C:\\Users\\marca\\Dev\\codename-boquete\\Fime.db");
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
                entity.HasNoKey();

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
