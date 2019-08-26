# 5) Modelos Delimitados

## Vídeo 5.1 - Modelos

Na última aula, além do banco de dados CasaDoCodigo, passamos a contar também com o novo banco de dados CasaDoCodigo.Catalogo, contendo apenas as tabelas `Produto` e `Categoria`. Essas tabelas fazem parte de ambos os bancos de dados. Esse tipo de duplicação pode parecer ruim, mas na verdade esse tipo de separação permite que que a base de dados da área de Catálogo possa evoluir de forma independente das demais áreas. A ideia é que as modificações no banco de dados do Catalogo não afetem outros bancos de dados.

Nesta aula veremos como isolar também o modelo do catálogo, que funcionará de forma independente dos modelos das outras áreas da aplicação.






\Areas\Catalogo\Controllers\HomeController.cs
```csharp
using CasaDoCodigo.Areas.Catalogo.Data.Repositories;
```

\Areas\Catalogo\Data\CatalogoDbContext.cs
```csharp
﻿using CasaDoCodigo.Models;
﻿using CasaDoCodigo.Areas.Catalogo.Models;
```

\Areas\Catalogo\Data\Repositories\BaseRepository.cs

```csharp
public abstract class BaseRepository<T> where T : BaseModel
{
    protected readonly IConfiguration configuration;
    protected readonly CatalogoDbContext contexto;
    protected readonly DbSet<T> dbSet;

    public BaseRepository(IConfiguration configuration,
        CatalogoDbContext contexto)
    {
        this.configuration = configuration;
        this.contexto = contexto;
        dbSet = contexto.Set<T>();
    }
}
```


### Classes copiadas para Área de Catálogo

*** Arquivos:
* \Areas\Catalogo\Data\Repositories\ProdutoRepository.cs
* \Areas\Catalogo\Models\BaseModel.cs
* \Areas\Catalogo\Models\Categoria.cs
* \Areas\Catalogo\Models\Produto.cs





### Mudar o namespace de CasaDoCodigo.Models para CasaDoCodigo.Areas.Catalogo.Models

*** Arquivos:
* \Areas\Catalogo\Models\ViewModels\BuscaProdutosViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CarrosselPaginaViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CarrosselViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CategoriasViewModel.cs
* \Areas\Catalogo\ViewComponents\CarrosselPaginaViewComponent.cs
* \Areas\Catalogo\ViewComponents\CarrosselViewComponent.cs
* \Areas\Catalogo\ViewComponents\CategoriasViewComponent.cs
* \Areas\Catalogo\Views\Home\_NavegacaoCarrossel.cshtml
* \Areas\Catalogo\Views\Home\_ProdutoCard.cshtml

```csharp
using CasaDoCodigo.Areas.Catalogo.Models;
```






### Mudar o namespace de CasaDoCodigo..Models.ViewModels para CasaDoCodigo.Areas.Catalogo.Models.ViewModels

**Arquivos:**
* \Areas\Catalogo\Views\Home\Index.cshtml
* \Areas\Catalogo\Views\Home\_FormularioBusca.cshtml

```csharp
@using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
```







**Arquivo:**
\Areas\Catalogo\Views\Home\Components\ProdutoCard\Default.cshtml

```csharp
@model CasaDoCodigo.Areas.Catalogo.Models.Produto;
```









### Registrando Injeção de Dependência para 2 Repositórios de Produtos


**Arquivo: \Startup.cs**

```csharp
services.AddTransient<Repositories.IProdutoRepository
, Repositories.ProdutoRepository>();
 
services.AddTransient<Areas.Catalogo.Data.Repositories.IProdutoRepository
, Areas.Catalogo.Data.Repositories.ProdutoRepository>();
```
