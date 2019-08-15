using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CasaDoCodigo.Areas.Catalogo.Controllers
{
    [Area("Catalogo")]
    public class HomeController : Controller
    {
        private readonly IProdutoRepository produtoRepository;

        public HomeController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string pesquisa)
        {
            return View("/Areas/Catalogo/Views/Home/Index.cshtml", await produtoRepository.GetProdutosAsync(pesquisa));
        }
    }
}
