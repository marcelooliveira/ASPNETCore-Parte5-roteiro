using System.ComponentModel.DataAnnotations;

namespace CasaDoCodigo.Models
{
    public class Produto : BaseModel
    {
        public Produto()
        {

        }

        public int? CategoriaId { get; private set; }

        [Required]
        public Categoria Categoria { get; private set; }
        [Required]
        public string Codigo { get; private set; }
        [Required]
        public string Nome { get; private set; }
        [Required]
        public decimal Preco { get; private set; }

        public Produto(int id, string codigo, string nome, decimal preco, Categoria categoria) 
            : this(codigo, nome, preco, categoria)
        {
            Id = id;
        }

        public Produto(string codigo, string nome, decimal preco, Categoria categoria)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
            this.Categoria = categoria;
        }
    }
}
