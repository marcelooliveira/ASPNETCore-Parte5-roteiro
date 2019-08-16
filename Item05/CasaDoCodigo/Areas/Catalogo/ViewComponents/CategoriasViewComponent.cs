using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
using CasaDoCodigo.Areas.Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Areas.Catalogo.ViewComponents
{
    public class CategoriasViewComponent : ViewComponent
    {
        const int TamanhoPagina = 4;
        public CategoriasViewComponent()
        {
        }

        public IViewComponentResult Invoke(List<Produto> produtos)
        {
            var categorias = produtos
                .Select(p => p.Categoria)
                .Distinct()
                .ToList();
            return View("Default", new CategoriasViewModel(categorias, produtos, TamanhoPagina));
        }
    }
}
