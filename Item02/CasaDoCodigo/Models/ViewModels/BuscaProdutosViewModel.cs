using System.Collections.Generic;

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
