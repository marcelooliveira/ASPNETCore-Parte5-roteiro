using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Cadastro.Controllers
{
    [Area("Cadastro")]
    public class CadastroController : Controller
    {
        private readonly IPedidoRepository pedidoRepository;

        public CadastroController(IPedidoRepository pedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var pedido = await pedidoRepository.GetPedidoAsync();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            pedido.Cadastro.Nome = User.FindFirst("name")?.Value;
            pedido.Cadastro.Email = User.FindFirst("email")?.Value;

            return View(pedido.Cadastro);
        }
    }
}

