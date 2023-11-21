using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaHotel.Models;

namespace SistemaHotel.Data;

public partial class Database : IdentityDbContext
{
    public Database()
    {
    }

    public Database(DbContextOptions<Database> options)
        : base(options)
    {
    }

    public virtual DbSet<CheckIn> CheckIn { get; set; }

    public virtual DbSet<CheckOut> CheckOut { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Cobro> Cobro { get; set; }

    public virtual DbSet<Habitacion> Habitacion { get; set; }

    public virtual DbSet<Huesped> Huesped { get; set; }

    public virtual DbSet<InventarioHabitacion> InventarioHabitacion { get; set; }

    public virtual DbSet<InventarioReposicion> InventarioReposicion { get; set; }

    public virtual DbSet<ItemHabitacion> ItemHabitacion { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<PagoPos> PagoPos { get; set; }

    public virtual DbSet<PagoQr> PagoQr { get; set; }

    public virtual DbSet<PagoStripe> PagoStripe { get; set; }

    public virtual DbSet<PaquetePromocional> PaquetePromocional { get; set; }

    public virtual DbSet<Reserva> Reserva { get; set; }

    public virtual DbSet<TipoHabitacion> TipoHabitacion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ReservasHotelDb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CheckIn>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHabitacion");
            entity.Property(e => e.IdHuesped)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHuesped");
            entity.Property(e => e.Ingreso)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ingreso");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany()
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CheckIn_idHabitacion_fkey");

            entity.HasOne(d => d.IdHuespedNavigation).WithMany()
                .HasForeignKey(d => d.IdHuesped)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CheckIn_idHuesped_fkey");
        });

        modelBuilder.Entity<CheckOut>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHabitacion");
            entity.Property(e => e.IdHuesped)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHuesped");
            entity.Property(e => e.Salida)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("salida");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany()
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CheckOut_idHabitacion_fkey");

            entity.HasOne(d => d.IdHuespedNavigation).WithMany()
                .HasForeignKey(d => d.IdHuesped)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CheckOut_idHuesped_fkey");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cliente_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(128)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .HasColumnName("email");
            entity.Property(e => e.Genero)
                .HasMaxLength(3)
                .HasColumnName("genero");
            entity.Property(e => e.Nombres)
                .HasMaxLength(128)
                .HasColumnName("nombres");
            entity.Property(e => e.NroRazonSocial)
                .HasMaxLength(64)
                .HasColumnName("nroRazonSocial");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(128)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(16)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Cobro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cobro_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(64)
                .HasColumnName("estado");
            entity.Property(e => e.IdReserva)
                .ValueGeneratedOnAdd()
                .HasColumnName("idReserva");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Cobro)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Cobro_idReserva_fkey");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Habitacion_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Habilitado)
                .HasDefaultValueSql("true")
                .HasColumnName("habilitado");
            entity.Property(e => e.IdTipoHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idTipoHabitacion");
            entity.Property(e => e.Nro)
                .HasMaxLength(16)
                .HasColumnName("nro");
            entity.Property(e => e.Reservado)
                .HasDefaultValueSql("false")
                .HasColumnName("reservado");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitacion)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Habitacion_idTipoHabitacion_fkey");
        });

        modelBuilder.Entity<Huesped>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Huesped_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(128)
                .HasColumnName("apellidos");
            entity.Property(e => e.DocIdentidad)
                .HasMaxLength(32)
                .HasColumnName("docIdentidad");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(16)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombres)
                .HasMaxLength(128)
                .HasColumnName("nombres");
            entity.Property(e => e.TipoDocIdentidad)
                .HasMaxLength(16)
                .HasColumnName("tipoDocIdentidad");
        });

        modelBuilder.Entity<InventarioHabitacion>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHabitacion");
            entity.Property(e => e.IdItem)
                .ValueGeneratedOnAdd()
                .HasColumnName("idItem");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany()
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InventarioHabitacion_idHabitacion_fkey");

            entity.HasOne(d => d.IdItemNavigation).WithMany()
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InventarioHabitacion_idItem_fkey");
        });

        modelBuilder.Entity<InventarioReposicion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InventarioReposicion_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHabitacion");
            entity.Property(e => e.IdItemHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idItemHabitacion");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.InventarioReposicion)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InventarioReposicion_idHabitacion_fkey");

            entity.HasOne(d => d.IdItemHabitacionNavigation).WithMany(p => p.InventarioReposicion)
                .HasForeignKey(d => d.IdItemHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("InventarioReposicion_idItemHabitacion_fkey");
        });

        modelBuilder.Entity<ItemHabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ItemHabitacion_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(128)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pago_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCobro)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCobro");
            entity.Property(e => e.Monto).HasColumnName("monto");

            entity.HasOne(d => d.IdCobroNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdCobro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pago_idCobro_fkey");
        });

        modelBuilder.Entity<PagoPos>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Autorizacion)
                .HasMaxLength(32)
                .HasColumnName("autorizacion");
            entity.Property(e => e.IdPago)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPago");
            entity.Property(e => e.Operacion)
                .HasMaxLength(32)
                .HasColumnName("operacion");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(16)
                .HasColumnName("tipoTarjeta");
            entity.Property(e => e.UltimosDigTarjeta).HasColumnName("ultimosDigTarjeta");

            entity.HasOne(d => d.IdPagoNavigation).WithMany()
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PagoPos_idPago_fkey");
        });

        modelBuilder.Entity<PagoQr>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.IdPago)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPago");
            entity.Property(e => e.NroTransaccion)
                .HasMaxLength(128)
                .HasColumnName("nroTransaccion");

            entity.HasOne(d => d.IdPagoNavigation).WithMany()
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PagoQr_idPago_fkey");
        });

        modelBuilder.Entity<PagoStripe>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .HasColumnName("estado");
            entity.Property(e => e.IdPago)
                .ValueGeneratedOnAdd()
                .HasColumnName("idPago");
            entity.Property(e => e.IdStripe)
                .HasMaxLength(64)
                .HasColumnName("idStripe");

            entity.HasOne(d => d.IdPagoNavigation).WithMany()
                .HasForeignKey(d => d.IdPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PagoStripe_idPago_fkey");
        });

        modelBuilder.Entity<PaquetePromocional>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PaquetePromocional_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaDisponibleFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaDisponibleFin");
            entity.Property(e => e.FechaDisponibleInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaDisponibleInicio");
            entity.Property(e => e.Habilitado)
                .HasDefaultValueSql("false")
                .HasColumnName("habilitado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(128)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasDefaultValueSql("0.00")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reserva_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(32)
                .HasDefaultValueSql("'RESERVADO'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fechaReserva");
            entity.Property(e => e.FinReserva)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("finReserva");
            entity.Property(e => e.IdCliente)
                .ValueGeneratedOnAdd()
                .HasColumnName("idCliente");
            entity.Property(e => e.IdHabitacion)
                .ValueGeneratedOnAdd()
                .HasColumnName("idHabitacion");
            entity.Property(e => e.InicioReserva)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inicioReserva");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reserva_idCliente_fkey");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reserva)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reserva_idHabitacion_fkey");
        });

        modelBuilder.Entity<TipoHabitacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TipoHabitacion_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(256)
                .HasColumnName("descripcion");
            entity.Property(e => e.HuespedesPermitidos).HasColumnName("huespedesPermitidos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(128)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioNoche).HasColumnName("precioNoche");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
