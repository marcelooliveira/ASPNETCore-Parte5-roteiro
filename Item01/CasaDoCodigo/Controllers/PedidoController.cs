using CasaDoCodigo.Areas.Identity.Data;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly UserManager<AppIdentityUser> userManager;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            UserManager<AppIdentityUser> userManager)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.userManager = userManager;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                var usuario = await userManager.GetUserAsync(this.User);

                usuario.Email = cadastro.Email;
                usuario.Telefone = cadastro.Telefone;
                usuario.Nome = cadastro.Nome;
                usuario.Endereco = cadastro.Endereco;
                usuario.Complemento = cadastro.Complemento;
                usuario.Bairro = cadastro.Bairro;
                usuario.Municipio = cadastro.Municipio;
                usuario.UF = cadastro.UF;
                usuario.CEP = cadastro.CEP;

                await userManager.UpdateAsync(usuario);

                return View(await pedidoRepository.UpdateCadastroAsync(cadastro));
            }
            return RedirectToAction("Cadastro");
        }
    }
}
