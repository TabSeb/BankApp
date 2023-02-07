using Microsoft.EntityFrameworkCore;
using BancoApp.Models;
using Microsoft.Extensions.Hosting;

namespace BancoApp.Models
{
    public class BankContext :DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        

        public DbSet<Producto> Productos { get; set; }
       
        public DbSet<SolicitudPrestamo> SolicitudPrestamos { get; set; }
        public DbSet<TarjetaCredito> tarjetaCreditos { get; set; }

        public DbSet<Restriccion> Restricciones { get; set; }

        public DbSet<SolicitudPaquete> SolicitudPaquetes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Cliente>()
            .ToTable("cliente")
            .HasDiscriminator<string>("TipoPersona")
            .HasValue<PersonaFisica>("Fisica")
            .HasValue<PersonaJuridica>("Juridica");


            modelBuilder.Entity<Producto>().ToTable("Producto")
                .HasDiscriminator<string>("TipoProducto")
                .HasValue<Paquete>("Paquete")
                .HasValue<Prestamo>("Prestamo");
            modelBuilder.Entity<Producto>()
            .HasOne(p => p.Cliente)
            .WithMany(b => b.Producto);

            modelBuilder.Entity<SolicitudPrestamo>().ToTable("SolicitudPrestamo");
            modelBuilder.Entity<SolicitudPrestamo>()
            .HasOne(p => p.Cliente)
            .WithMany(b => b.SolicitudPrestamos);
            modelBuilder.Entity<TarjetaCredito>().ToTable("TarjetaCredito");
            modelBuilder.Entity<TarjetaCredito>()
            .HasOne(p => p.Paquete)
            .WithMany(b => b.tarjetas);
            modelBuilder.Entity<Restriccion>().ToTable("Restriccion");
            modelBuilder.Entity<Restriccion>()
            .HasOne(p => p.Cliente)
            .WithOne(b => b.Restriccion);


            modelBuilder.Entity<SolicitudPaquete>().ToTable("SolicitudPaquete");
            modelBuilder.Entity<SolicitudPaquete>()
            .HasOne(p => p.Cliente)
            .WithMany(b => b.SolicitudPaquetes);

        }


        public DbSet<BancoApp.Models.PersonaFisica> PersonaFisica { get; set; } = default!;
        public DbSet<BancoApp.Models.PersonaJuridica> PersonaJuridica { get; set; } = default!;


        public DbSet<BancoApp.Models.Prestamo> Prestamo { get; set; } = default!;


        public DbSet<BancoApp.Models.Paquete> Paquete { get; set; } = default!;
    }
}
