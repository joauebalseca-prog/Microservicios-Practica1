using Inventario.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Data
{
    public class InventarioDBContext : DbContext
    {
        public InventarioDBContext(DbContextOptions<InventarioDBContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Existencias> Existencias { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Claves primarias
            modelBuilder.Entity<Bodega>()
                .HasKey(b => b.IdBodega);

            modelBuilder.Entity<Existencias>()
                .HasKey(e => e.IdExistencias);

            modelBuilder.Entity<MovimientoInventario>()
                .HasKey(m => m.IdMovimientoInv);

            modelBuilder.Entity<Stock>()
                .HasKey(s => s.IdStock);

            // Relaciones con Bodega
            modelBuilder.Entity<Existencias>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(e => e.IdBodega);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(m => m.IdBodega);

            modelBuilder.Entity<Stock>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(s => s.IdBodega);
        }
    }
}