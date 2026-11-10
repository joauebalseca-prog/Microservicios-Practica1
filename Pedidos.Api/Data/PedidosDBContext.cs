using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Models;

namespace Pedidos.Api.Data
{
    public class PedidosDBContext : DbContext
    {
        public PedidosDBContext(DbContextOptions<PedidosDBContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Claves primarias
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.IdPedido);

            modelBuilder.Entity<DetallePedido>()
                .HasKey(d => d.IdDetallePedido);

            // Relación entre Cliente y Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne<Cliente>()
                .WithMany()
                .HasForeignKey(p => p.IdCliente);

            // Relación entre Pedido y DetallePedido
            modelBuilder.Entity<DetallePedido>()
                .HasOne<Pedido>()
                .WithMany()
                .HasForeignKey(d => d.IdPedido);
        }
    }
}