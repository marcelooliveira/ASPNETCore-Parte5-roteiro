using CasaDoCodigo.Areas.Catalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Areas.Catalogo.Data
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Categoria>().HasKey(t => t.Id);
            modelBuilder.Entity<Produto>().HasKey(t => t.Id);
        }
    }

}
