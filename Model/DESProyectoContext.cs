using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace codename_boquete.Model
{
    public partial class DESProyectoContext : DbContext
    {
        public DESProyectoContext()
        {
        }

        public DESProyectoContext(DbContextOptions<DESProyectoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CsrfDataNumber> CsrfDataNumbers { get; set; } = null!;
        public virtual DbSet<CsrfDato> CsrfDatos { get; set; } = null!;
        public virtual DbSet<CsrfReporteDeFuga> CsrfReporteDeFugas { get; set; } = null!;
        public virtual DbSet<CsrfRetrabajadore> CsrfRetrabajadores { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=DESProyecto;User=SA;Password=arribaLasChivas10;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CsrfDataNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_DataNumber");

                entity.Property(e => e.Cantidad).IsUnicode(false);

                entity.Property(e => e.Color).IsUnicode(false);

                entity.Property(e => e.Modelo).IsUnicode(false);
            });

            modelBuilder.Entity<CsrfDato>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_Datos");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCoil)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtraCausaDeFuga)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtroTipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OtroTipoDeFuga)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ProveedorTxv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ProveedorTXV");

                entity.Property(e => e.Proveedores)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Retrabajadores)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Seccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soldadores)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoExtra)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoFugaVisible)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CsrfReporteDeFuga>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_ReporteDeFugas");

                entity.Property(e => e.Area)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CoilRechazado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comentarios)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.CostXunit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CostXUnit");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FugaFalsa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCoil)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumSerie)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Numero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Operador)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PiezaRetrabajada)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Seccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soldador)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoExtra)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CsrfRetrabajadore>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CSRF_Retrabajadores");

                entity.Property(e => e.Induccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nomina)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReparacionAb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ReparacionAB");

                entity.Property(e => e.ReparacionCamara)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoldaduraManual)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
