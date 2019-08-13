using CasaDoCodigo.Areas.Catalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Catalogo.Controllers
{
    [Area("Catalogo")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }
            
        public async Task<IActionResult> Index(string pesquisa)
        {
            return View(await produtoRepository.GetProdutosAsync(pesquisa));
        }
    }
}
