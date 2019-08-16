using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
using CasaDoCodigo.Areas.Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Areas.Catalogo.ViewComponents
{
    public class CarrosselPaginaViewComponent : ViewComponent
    {
        public CarrosselPaginaViewComponent()
        {

        }

        public IViewComponentResult Invoke(List<Produto> produtosNaCategoria, int indicePagina, int tamanhoPagina)
        {
            var produtosNaPagina =
                produtosNaCategoria
                .Skip(indicePagina * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToList();

            return View("Default",
                new CarrosselPaginaViewModel(produtosNaPagina, indicePagina));
        }
    }
}
