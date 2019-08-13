using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        public CarrinhoViewModel(IList<ItemPedido> itens)
        {
            Itens = itens;
        }

        public CarrinhoViewModel(IList<ItemPedido> itens, bool ehResumo)
        {
            Itens = itens;
            EhResumo = ehResumo;
        }

        public IList<ItemPedido> Itens { get; }
        public bool EhResumo { get; set; }

        public decimal Total => Itens.Sum(i => i.Quantidade * i.PrecoUnitario);
    }
}
