﻿using CasaDoCodigo.Areas.Catalogo.Models;
using CasaDoCodigo.Areas.Catalogo.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCodigo.Areas.Catalogo.Data
{
    public class SeedCatalogo : ISeedCatalogo
    {
        public async Task InicializaDBAsync(IServiceProvider provider)
        {
            var contexto = provider.GetService<CatalogoDbContext>();

            await contexto.Database.MigrateAsync();

            if (await contexto.Set<Produto>().AnyAsync())
            {
                return;
            }

            List<Livro> livros = await GetLivrosAsync();

            var produtoRepository = provider.GetService<IProdutoRepository>();
            await produtoRepository.SaveProdutosAsync(livros);
        }

        private async Task<List<Livro>> GetLivrosAsync()
        {
            var json = await File.ReadAllTextAsync("livros.json");
            return JsonConvert.DeserializeObject<List<Livro>>(json);
        }
    }
}
