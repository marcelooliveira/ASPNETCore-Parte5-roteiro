using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Areas.Carrinho.ViewComponents
{
    public class CarrinhoListViewComponent : ViewComponent
    {
        public CarrinhoListViewComponent()
        {

        }

        public IViewComponentResult Invoke(CarrinhoViewModel carrinhoViewModel, bool ehResumo = false)
        {
            var itens = carrinhoViewModel.Itens;

            return View("Default", new CarrinhoViewModel(itens, ehResumo));
        }
    }
}
