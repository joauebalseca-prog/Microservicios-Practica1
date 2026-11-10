using Catalogo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Data
{
    public class CatalogoDBContext : DbContext
    {
        public CatalogoDBContext(DbContextOptions<CatalogoDBContext> options)
            : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Marca>()
                .HasKey(m => m.IdMarca);

            modelBuilder.Entity<Categoria>()
                .HasKey(c => c.IdCategoria);

            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Producto>()
                .HasOne<Marca>()
                .WithMany()
                .HasForeignKey(p => p.IdMarca);

            modelBuilder.Entity<Producto>()
                .HasOne<Categoria>()
                .WithMany()
                .HasForeignKey(p => p.IdCategoria);
        }
    }
}