using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaProdutosViewModel
    {
        public BuscaProdutosViewModel(List<Produto> produtos, string pesquisa)
        {
            Produtos = produtos;
            Pesquisa = pesquisa;
        }

        public List<Produto> Produtos { get; }
        public string Pesquisa { get; set; }
    }
}
