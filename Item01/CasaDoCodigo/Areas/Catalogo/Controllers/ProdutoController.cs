using CasaDoCodigo.Repositories;
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

        public async Task<IActionResult> Carrossel()
        {
            return View(await produtoRepository.GetProdutosAsync());
        }

        public async Task<IActionResult> BuscaProdutos(string pesquisa)
        {
            return View(await produtoRepository.GetProdutosAsync(pesquisa));
        }
    }
}
