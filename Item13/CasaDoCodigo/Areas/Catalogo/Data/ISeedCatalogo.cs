using System;
using System.Threading.Tasks;

namespace CasaDoCodigo.Areas.Catalogo.Data
{
    public interface ISeedCatalogo
    {
        Task InicializaDBAsync(IServiceProvider provider);
    }
}