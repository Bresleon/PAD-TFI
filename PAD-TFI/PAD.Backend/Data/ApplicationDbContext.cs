using Microsoft.EntityFrameworkCore;
using PAD.Backend.Models.Entidades;

namespace PAD.Backend.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Modelo> Modelos { get; set; }
    public DbSet<Patente> Patentes { get; set; }
    public DbSet<Titular> Titulares { get; set; }
    public DbSet<Transaccion> Transacciones { get; set; }
    public DbSet<Vehiculo> Vehiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigurarMarca(modelBuilder);
        ConfigurarModelo(modelBuilder);
        ConfigurarVehiculo(modelBuilder);
        ConfigurarTitular(modelBuilder);
        ConfigurarPatente(modelBuilder);
        ConfigurarTransaccion(modelBuilder);
    }

    private void ConfigurarMarca(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marca>(entity =>
        {
            entity.ToTable("marcas");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            entity.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasData(Iniciales.Instance.Marcas);
        });
    }

    private void ConfigurarModelo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.ToTable("modelos");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();
            entity.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasOne(m => m.Marca)
                .WithMany(ma => ma.Modelos)
                .HasForeignKey(m => m.MarcaId);

            entity.HasData(Iniciales.Instance.Modelos);
        });
    }

    private void ConfigurarVehiculo(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.ToTable("vehiculos");
            entity.HasKey(v => v.Id);
            entity.Property(v => v.Id).ValueGeneratedOnAdd();
            entity.Property(v => v.Precio)
                .HasColumnType("decimal(18,2)");
            entity.Property(v => v.NumeroChasis)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(v => v.NumeroMotor)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(v => v.Categoria)
                .HasConversion<string>();
            //entity.HasOne(v => v.Marca)
            //    .WithMany()
            //    .HasForeignKey(v => v.MarcaId)
            //    .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(v => v.Modelo)
                .WithMany(m => m.Vehiculos)
                .HasForeignKey(v => v.ModeloId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Iniciales.Instance.Vehiculos);
        });
    }

    private void ConfigurarTitular(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Titular>(entity =>
        {
            entity.ToTable("titulares");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(t => t.Apellido)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(t => t.Dni)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(t => t.Email)
                .HasMaxLength(100);
            entity.Property(t => t.Telefono)
                .HasMaxLength(50);

            entity.HasData(Iniciales.Instance.Titulares);
        });
    }

    private void ConfigurarPatente(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patente>(entity =>
        {
            entity.ToTable("patentes");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).ValueGeneratedOnAdd();
            entity.Property(p => p.NumeroPatente)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(p => p.FechaEmision)
                .IsRequired();
            entity.Property(p => p.Ejemplar)
                .HasConversion<string>();
            entity.HasOne(p => p.Vehiculo)
                .WithMany()
                .HasForeignKey(p => p.VehiculoId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(p => p.Titular)
                .WithMany(t => t.Patentes)
                .HasForeignKey(p => p.TitularId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Iniciales.Instance.Patentes);
        });
    }

    private void ConfigurarTransaccion(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.ToTable("transacciones");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();
            entity.Property(t => t.Costo)
                .HasColumnType("decimal(18,2)");
            entity.Property(t => t.TipoTransaccion)
                .HasConversion<string>();
            entity.Property(t => t.Fecha)
                .IsRequired();
            entity.HasOne(t => t.Patente)
                .WithMany(p => p.Transacciones)
                .HasForeignKey(t => t.PatenteId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(t => t.TitularOrigen)
                .WithMany(ti => ti.Transacciones)
                .HasForeignKey(t => t.TitularOrigenId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(t => t.TitularDestino)
                .WithMany()
                .HasForeignKey(t => t.TitularDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(Iniciales.Instance.Transacciones);
        });
    }
}