# 5) Modelos Delimitados

## Vídeo 5.1 - Novo Modelos de Catálogo

Na última aula, além do banco de dados CasaDoCodigo, passamos a contar também com o novo banco de dados CasaDoCodigo.Catalogo, contendo apenas as tabelas `Produto` e `Categoria`. Essas tabelas agora existem em ambos os bancos de dados. Esse tipo de duplicação pode parecer ruim, mas na verdade esse tipo de separação permite que que a base de dados da área de Catálogo possa evoluir de forma independente das demais áreas. A ideia é que as modificações no banco de dados do Catalogo não afetem outros bancos de dados.

Nesta aula veremos como isolar também o modelo do catálogo, que funcionará de forma independente dos modelos das outras áreas da aplicação.

Vamos copiar as classes do modelo da pasta /Models/ para a nova pasta /Areas/Catalogo/Models :

### Classes copiadas para Área de Catálogo

*** Arquivos:
* \Areas\Catalogo\Data\Repositories\BaseRepository.cs
* \Areas\Catalogo\Data\Repositories\ProdutoRepository.cs
* \Areas\Catalogo\Models\BaseModel.cs
* \Areas\Catalogo\Models\Categoria.cs
* \Areas\Catalogo\Models\Produto.cs

Com essa mudança, as classes e views da área de Catalogo precisam referenciar (isto é, apontar para) as novas classes do modelo da área:

### Mudar o namespace de CasaDoCodigo.Models para CasaDoCodigo.Areas.Catalogo.Models

**Arquivos:**
* \Areas\Catalogo\Models\ViewModels\BuscaProdutosViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CarrosselPaginaViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CarrosselViewModel.cs
* \Areas\Catalogo\Models\ViewModels\CategoriasViewModel.cs
* \Areas\Catalogo\ViewComponents\CarrosselPaginaViewComponent.cs
* \Areas\Catalogo\ViewComponents\CarrosselViewComponent.cs
* \Areas\Catalogo\ViewComponents\CategoriasViewComponent.cs
* \Areas\Catalogo\Views\Home\_NavegacaoCarrossel.cshtml
* \Areas\Catalogo\Views\Home\_ProdutoCard.cshtml
* \Areas\Catalogo\Views\Home\Index.cshtml
* \Areas\Catalogo\Views\Home\_FormularioBusca.cshtml
* \Areas\Catalogo\Views\Home\Components\ProdutoCard\Default.cshtml

```csharp
using CasaDoCodigo.Areas.Catalogo.Models;
```

## Vídeo 5.2 - Configurando o Novo Repositório de Produtos

Outra mudança é na classe base de repositório. Vamos ter que mudar a classe do contexto de ApplicationDbContext para CatalogoDbContext, para que o repositório de produtos da área de Catálogo possa acessar o banco de dados a partir do contexto da própria área de catálogo.

### Mudar a classe do contexto de ApplicationDbContext para CatalogoDbContext

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

Agora vamos mudar também o namespace da classe de repositório acessada pelo controller MVC da área de catálogo, apontando para o repositório da área:

### Mudar o namespace de CasaDoCodigo.Data.Repositories para CasaDoCodigo.Areas.Catalogo.Data.Repositories

\Areas\Catalogo\Controllers\HomeController.cs
```csharp
using CasaDoCodigo.Areas.Catalogo.Data.Repositories;
```

Por último, vamos modificar a classe `Startup` do projeto, para configurar a injeção de dependência para ambas as classes de repositório de produtos: tanto a classe `ProdutoRepository` que atende a aplicação como um todo quanto a classe `ProdutoRepository` que atende somente a área de Catálogo.

### Registrando Injeção de Dependência para 2 Repositórios de Produtos

**Arquivo: \Startup.cs**

```csharp
services.AddTransient<Repositories.IProdutoRepository
, Repositories.ProdutoRepository>();
 
services.AddTransient<Areas.Catalogo.Data.Repositories.IProdutoRepository
, Areas.Catalogo.Data.Repositories.ProdutoRepository>();
```

Note quem ambas as classes foram registradas como *transientes* (temporária), isto é, tal como explicamos no início desta sequência de cursos, cada vez que um objeto transiente (temporário) é requisitada por um objeto, uma nova instância é automaticamente criada.

## Vídeo 5.3 - Removendo produto e categoria




> arquivo: \ApplicationContext.cs

remover:

```csharp
modelBuilder.Entity<Categoria>().HasKey(t => t.Id);
modelBuilder.Entity<Produto>().HasKey(t => t.Id);
.
.
.
modelBuilder.Entity<ItemPedido>().HasOne(t => t.Produto);
```


> arquivo: \Areas\Carrinho\Views\Home\Index.cshtml

substituir:

```razor
<img class="img-produto-carrinho" src="/images/produtos/large_@(item.Produto.Codigo).jpg" />
```

por

```razor
<img class="img-produto-carrinho" src="/images/produtos/large_@(item.ProdutoCodigo).jpg" />
```

substituir:

```razor
<div class="col-md-3">@(item.Produto.Nome)</div>
```

por 

```razor
<div class="col-md-3">@(item.ProdutoNome)</div>
```

> arquivo: \Areas\Pedido\Views\Home\Index.cshtml

substituir:

```razor
<div>@item.Produto.Nome</div>
```

por 

```razor
<div>@item.ProdutoNome</div>
```

**Rodar as migrações:**

>PM> Add-Migration "ModeloSemProduto" -Context ApplicationDbContext
>PM> Update-Database -verbose -Context ApplicationDbContext 

> arquivo: \Models\ItemPedido.cs

Substituir:

```csharp
public Produto Produto { get; private set; }
```

Por:

```csharp
public string ProdutoCodigo { get; private set; }
[Required]
[DataMember]
public string ProdutoNome { get; private set; }
```

Substituir:

```csharp
public ItemPedido(Pedido pedido, Produto produto, int quantidade, decimal precoUnitario)
```

Por:

```csharp
public ItemPedido(Pedido pedido, string produtoCodigo, string produtoNome, int quantidade, decimal precoUnitario)
```

Substituir:

```csharp
Produto = produto;
```

Por:

```csharp
ProdutoCodigo = produtoCodigo;
ProdutoNome = produtoNome;
```

> arquivo: \Repositories\PedidoRepository.cs

Incluir o campo, parâmetro e inicialização para o repositório de produtos:

```csharp
private readonly IProdutoRepository produtoRepository;
```

substituir:

```csharp
var produto = await
    contexto.Set<Produto>()
    .Where(p => p.Codigo == codigo)
    .SingleOrDefaultAsync();
```

por:

```csharp
var produto = await produtoRepository.GetProdutoAsync(codigo);
```

substituir:

```csharp
	.Where(i => i.Produto.Codigo == codigo
```

por:

```csharp
	.Where(i => i.ProdutoCodigo == codigo
```

substituir:

```csharp
itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);
```
por:

```csharp
itemPedido = new ItemPedido(pedido, produto.Codigo, produto.Nome, 1, produto.Preco);
```


remover:

```csharp
	.ThenInclude(i => i.Produto)
	.ThenInclude(prod => prod.Categoria)
```

> arquivo: \Startup.cs

remover:

```csharp
services.AddTransient<IDataService, DataService>();
```

remover:

```csharp
services.AddTransient<Repositories.IProdutoRepository, Repositories.ProdutoRepository>();
services.AddTransient<Areas.Catalogo.Data.Repositories.IProdutoRepository, Areas.Catalogo.Data.Repositories.ProdutoRepository>();
```

adicionar:

```csharp
services.AddTransient<IProdutoRepository, ProdutoRepository>();
```

remover:

```csharp
var dataService = serviceProvider.GetRequiredService<IDataService>();
dataService.InicializaDBAsync(serviceProvider).Wait();
```