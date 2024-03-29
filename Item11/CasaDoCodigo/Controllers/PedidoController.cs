﻿using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
        }

        [Authorize]
        public async Task<IActionResult> Cadastro()
        {
            var pedido = await pedidoRepository.GetPedidoAsync();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }

            //var usuario = await userManager.GetUserAsync(this.User);

            //pedido.Cadastro.Email = usuario.Email;
            //pedido.Cadastro.Telefone = usuario.Telefone;
            //pedido.Cadastro.Nome = usuario.Nome;
            //pedido.Cadastro.Endereco = usuario.Endereco;
            //pedido.Cadastro.Complemento = usuario.Complemento;
            //pedido.Cadastro.Bairro = usuario.Bairro;
            //pedido.Cadastro.Municipio = usuario.Municipio;
            //pedido.Cadastro.UF = usuario.UF;
            //pedido.Cadastro.CEP = usuario.CEP;

            pedido.Cadastro.Nome = User.FindFirst("name")?.Value;
            pedido.Cadastro.Email = User.FindFirst("email")?.Value;

            return View(pedido.Cadastro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                //var usuario = await userManager.GetUserAsync(this.User);

                //usuario.Email       = cadastro.Email;
                //usuario.Telefone    = cadastro.Telefone;
                //usuario.Nome        = cadastro.Nome;
                //usuario.Endereco    = cadastro.Endereco;
                //usuario.Complemento = cadastro.Complemento;
                //usuario.Bairro      = cadastro.Bairro;
                //usuario.Municipio   = cadastro.Municipio;
                //usuario.UF          = cadastro.UF;
                //usuario.CEP         = cadastro.CEP;

                //await userManager.UpdateAsync(usuario);

                return View(await pedidoRepository.UpdateCadastroAsync(cadastro));
            }
            return RedirectToAction("Cadastro");
        }
    }
}
