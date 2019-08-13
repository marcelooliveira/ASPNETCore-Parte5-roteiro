using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Areas.Carrinho.ViewComponents
{
    public class CarrinhoItemViewComponent : ViewComponent
    {
        public CarrinhoItemViewComponent()
        {
        }

        public IViewComponentResult Invoke(ItemPedido item, bool ehResumo = false)
        {
            if (ehResumo == true)
            {
                return View("ResumoItem", item);
            }
            return View("Default", item);
        }
    }
}
