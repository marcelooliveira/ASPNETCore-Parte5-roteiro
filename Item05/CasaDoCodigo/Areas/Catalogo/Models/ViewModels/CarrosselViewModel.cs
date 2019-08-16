using CasaDoCodigo.Areas.Catalogo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Areas.Catalogo.Models.ViewModels
{
    public class CarrosselViewModel
    {
        public CarrosselViewModel()
        {

        }

        public CarrosselViewModel(Categoria categoria, List<Produto> produtos, int numPaginas, int tamanhoPagina)
        {
            Categoria = categoria;
            Produtos = produtos;
            NumPaginas = numPaginas;
            TamanhoPagina = tamanhoPagina;
        }

        public Categoria Categoria { get; set; }
        public List<Produto> Produtos { get; set; }
        public int NumPaginas { get; set; }
        public int TamanhoPagina { get; set; }
    }
}
