using CasaDoCodigo.Areas.Catalogo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Areas.Catalogo.Models.ViewModels
{
    public class CarrosselPaginaViewModel
    {
        public CarrosselPaginaViewModel()
        {

        }

        public CarrosselPaginaViewModel(List<Produto> produtos, int indicePagina)
        {
            Produtos = produtos;
            IndicePagina = indicePagina;
        }

        public List<Produto> Produtos { get; set; }
        public int IndicePagina { get; set; }
    }
}
