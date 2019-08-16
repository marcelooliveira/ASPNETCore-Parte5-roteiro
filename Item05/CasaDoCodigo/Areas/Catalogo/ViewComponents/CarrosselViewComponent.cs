using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
using CasaDoCodigo.Areas.Catalogo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Areas.Catalogo.ViewComponents
{
    public class CarrosselViewComponent : ViewComponent
    {
        public CarrosselViewComponent()
        {

        }

        public IViewComponentResult Invoke(Categoria categoria, List<Produto> produtos, int tamanhoPagina)
        {
            var produtosNaCategoria = produtos
                .Where(p => p.Categoria.Id == categoria.Id)
                .ToList();
            int pageCount = (int)Math.Ceiling((double)produtosNaCategoria.Count() / tamanhoPagina);

            return View("Default",
                new CarrosselViewModel(categoria, produtosNaCategoria, pageCount, tamanhoPagina));
        }
    }
}
