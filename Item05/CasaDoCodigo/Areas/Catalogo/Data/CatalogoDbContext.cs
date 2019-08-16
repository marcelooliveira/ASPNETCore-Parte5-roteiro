using CasaDoCodigo.Areas.Catalogo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Catalogo.Data
{
    //***OBS***
    //PARA A AULA 5, 
    //DROPAR O BANCO
    //APAGAR AS MIGRAÇÕES
    //RODAR NOVAMENTE AS MIGRAÇÕES ABAIXO

    //PM> Add-Migration "AreaCatalogo" -Context CatalogoDbContext -o "Areas/Catalogo/Data/Migrations"
    //PM> Update-Database -verbose -Context CatalogoDbContext 
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            var produtos = GetProdutos();
            var categorias =
                produtos.Select(p => p.Categoria).Distinct();

            builder.Entity<Categoria>(b =>
            {
                b.HasKey(t => t.Id);
                b.HasData(categorias);
            });

            builder.Entity<Produto>(b =>
            {
                b.HasKey(t => t.Id);
                b.HasData(
                produtos.Select(p =>
                        new
                        {
                            p.Id,
                            p.Codigo,
                            p.Nome,
                            p.Preco,
                            CategoriaId = p.Categoria.Id
                        }
                    ));
            });
            builder.Entity<Produto>();
        }

        private IEnumerable<Produto> GetProdutos()
        {
            var livros = GetLivros();

            var categorias = livros
                    .Select((l) => l.Categoria)
                    .Distinct()
                    .Select((nomeCategoria, i) =>
                    {
                        var c = new Categoria(nomeCategoria);
                        c.Id = i + 1;
                        return c; });

            var produtos =
                (from livro in livros
                join categoria in categorias
                    on livro.Categoria equals categoria.Nome
                select new Produto(livro.Codigo, livro.Nome, livro.Preco, categoria))
                .Select((p, i) =>
                    {
                        p.Id = i + 1;
                        return p;
                    })
                .ToList();

            return produtos;
        }

        private List<Livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            return JsonConvert.DeserializeObject<List<Livro>>(json);
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
