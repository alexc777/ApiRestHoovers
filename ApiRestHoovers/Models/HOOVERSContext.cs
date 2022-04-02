using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiRestHoovers.Models
{
    public partial class HOOVERSContext : DbContext
    {
        public HOOVERSContext()
        {
        }

        public HOOVERSContext(DbContextOptions<HOOVERSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Departamento> Departamentos { get; set; }
        public virtual DbSet<HttpMethod> HttpMethods { get; set; }
        public virtual DbSet<LogBitacora> LogBitacoras { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<Viaje> Viajes { get; set; }
        public virtual DbSet<VwViajeCliente> VwViajeClientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=HOOVERS;Integrated Security=True;Pooling=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDO");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ACTUALIZACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("DEPARTAMENTO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ACTUALIZACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<HttpMethod>(entity =>
            {
                entity.ToTable("HTTP_METHOD");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<LogBitacora>(entity =>
            {
                entity.ToTable("LOG_BITACORA");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.FechaOperacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_OPERACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMethod).HasColumnName("ID_METHOD");

                entity.Property(e => e.IdModule).HasColumnName("ID_MODULE");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO")
                    .HasDefaultValueSql("(@@servername)");

                entity.HasOne(d => d.IdMethodNavigation)
                    .WithMany(p => p.LogBitacoras)
                    .HasForeignKey(d => d.IdMethod)
                    .HasConstraintName("FK_BITA_METODO");

                entity.HasOne(d => d.IdModuleNavigation)
                    .WithMany(p => p.LogBitacoras)
                    .HasForeignKey(d => d.IdModule)
                    .HasConstraintName("FK_BITA_MODULO");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("MODULES");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.ToTable("TIPO_VEHICULO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ACTUALIZACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.ToTable("VEHICULO");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ACTUALIZACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdTipo)
                    .HasColumnName("ID_TIPO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Modelo).HasColumnName("MODELO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_TIPO");
            });

            modelBuilder.Entity<Viaje>(entity =>
            {
                entity.ToTable("VIAJE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DescripcionViaje)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_VIAJE")
                    .HasDefaultValueSql("('VIAJE DE NEGOCIO')");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_FIN")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaViaje)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_VIAJE");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdDeptoViaje)
                    .HasColumnName("ID_DEPTO_VIAJE")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdVehiculo).HasColumnName("ID_VEHICULO");

                entity.Property(e => e.PrecioViaje)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("PRECIO_VIAJE");

                entity.Property(e => e.ViajeRealizado)
                    .HasColumnName("VIAJE_REALIZADO")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_CLIENTE");

                entity.HasOne(d => d.IdDeptoViajeNavigation)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.IdDeptoViaje)
                    .HasConstraintName("FK_DEPARTAMENTO");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Viajes)
                    .HasForeignKey(d => d.IdVehiculo)
                    .HasConstraintName("FK_VEHICULO");
            });

            modelBuilder.Entity<VwViajeCliente>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_VIAJE_CLIENTE");

                entity.Property(e => e.DescripcionViaje)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION_VIAJE");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_FIN");

                entity.Property(e => e.FechaViaje)
                    .HasColumnType("date")
                    .HasColumnName("FECHA_VIAJE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdDeptoViaje).HasColumnName("ID_DEPTO_VIAJE");

                entity.Property(e => e.IdVehiculo).HasColumnName("ID_VEHICULO");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(501)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.PrecioViaje)
                    .HasColumnType("numeric(16, 2)")
                    .HasColumnName("PRECIO_VIAJE");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");

                entity.Property(e => e.ViajeRealizado).HasColumnName("VIAJE_REALIZADO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
