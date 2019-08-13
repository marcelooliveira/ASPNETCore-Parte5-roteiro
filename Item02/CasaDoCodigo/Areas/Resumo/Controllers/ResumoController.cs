using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Resumo.Controllers
{
    [Area("Resumo")]
    public class ResumoController : Controller
    {
        private readonly IPedidoRepository resumoController;

        public ResumoController(IPedidoRepository pedidoRepository)
        {
            this.resumoController = pedidoRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Index(CasaDoCodigo.Models.Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                return View(await resumoController.UpdateCadastroAsync(cadastro));
            }
            return RedirectToAction("Cadastro");
        }
    }
}
