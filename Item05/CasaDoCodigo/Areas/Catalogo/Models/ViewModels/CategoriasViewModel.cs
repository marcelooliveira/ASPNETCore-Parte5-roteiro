using CasaDoCodigo.Areas.Catalogo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Areas.Catalogo.Models.ViewModels
{
    public class CategoriasViewModel
    {
        public CategoriasViewModel()
        {

        }

        public CategoriasViewModel(List<Categoria> categorias, List<Produto> produtos, int tamanhoPagina)
        {
            Categorias = categorias;
            Produtos = produtos;
            TamanhoPagina = tamanhoPagina;
        }

        public List<Categoria> Categorias { get; set; }
        public List<Produto> Produtos { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
