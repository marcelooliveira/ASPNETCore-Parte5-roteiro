using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Catalogo.Models
{
    public class Categoria : BaseModel
    {
        public Categoria() { }

        public Categoria(string nome)
        {
            Nome = nome;
        }

        [Required]
        public string Nome { get; private set; }
    }
}
