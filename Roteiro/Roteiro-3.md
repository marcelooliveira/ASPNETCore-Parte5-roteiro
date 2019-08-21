# 3) View Components

Movendo BuscaProdutosViewModel para \Areas\Catalogo\Models\ViewModels:

*** arquivo: \Areas\Catalogo\Models\ViewModels\BuscaProdutosViewModel.cs***

```csharp
﻿    public class BuscaProdutosViewModel
{
    public BuscaProdutosViewModel(List<Produto> produtos, string pesquisa)
    {
        Produtos = produtos;
        Pesquisa = pesquisa;
    }

    public List<Produto> Produtos { get; }
    public string Pesquisa { get; set; }
}
```


## ViewComponent: Categorias

*** arquivo: \Areas\Catalogo\Views\Home\Index.cshtml***

ADICIONAR:

@addTagHelper *, CasaDoCodigo.MVC

REMOVER:
```csharp
-@{
    var produtos = Model.Produtos;

    var categorias =
        produtos
            .Select(m => m.Categoria)
            .Distinct();
}

@foreach (var categoria in categorias)
{
    var produtosNaCategoria =
        produtos
        .Where(p => p.Categoria.Id == categoria.Id);

    <h3>@categoria.Nome</h3>

    <div id="my-carousel-@categoria.Id" class="carousel slide" data-ride="carousel">
        <!-- Wrapper for slides -->
        <div class="carousel-inner" role="listbox">
            @{
                const int TAMANHO_PAGINA = 4;

                int paginas = (int)Math.Ceiling((double)produtosNaCategoria.Count() / TAMANHO_PAGINA);
            }
            @for (int pagina = 0; pagina < paginas; pagina++)
            {

                <div class="item @(pagina == 0 ? "active" : "")">
                    <div class="row">
                        @{
                            var produtosDaPagina = produtosNaCategoria.Skip(pagina * TAMANHO_PAGINA).Take(TAMANHO_PAGINA);
                        }
                        @foreach (var produto in produtosDaPagina)
                        {
                            <partial name="_ProdutoCard" model="@produto" />
                        }
                    </div>
                </div>

            }
        </div>
        <!-- Controls -->
        <partial name="_NavegacaoCarrossel" model="@categoria" />
    </div>
}
```

ADICIONAR:

```razor
<vc:categorias produtos="@Model.Produtos"></vc:categorias>
```

*** arquivo: \Areas\Catalogo\Models\ViewModels\CategoriasViewModel.cs***

```csharp
public class CategoriasViewModel
{
    public CategoriasViewModel()
    {

    }

    public CategoriasViewModel(List<Categoria> categorias, List<Produto> produtos, int tamanhoPagina)
    {
        Categorias = categorias;
        Produtos = produtos;
        TamanhoPagina = tamanhoPagina;
    }

    public List<Categoria> Categorias { get; set; }
    public List<Produto> Produtos { get; set; }
    public int TamanhoPagina { get; set; }
}
```

*** arquivo: \Areas\Catalogo\ViewComponents\CategoriasViewComponent.cs***


```csharp
public class CategoriasViewComponent : ViewComponent
{
    const int TamanhoPagina = 4;
    public CategoriasViewComponent()
    {
    }

    public IViewComponentResult Invoke(List<Produto> produtos)
    {
        var categorias = produtos
            .Select(p => p.Categoria)
            .Distinct()
            .ToList();
        return View("Default", new CategoriasViewModel(categorias, produtos, TamanhoPagina));
    }
}
```


*** arquivo: \Areas\Catalogo\Views\Home\Components\Categorias\Default.cshtml***

```csharp
﻿@using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
@addTagHelper *, CasaDoCodigo.MVC
@model CategoriasViewModel

<div class="container">
    @foreach (var categoria in Model.Categorias)
    {
        <vc:carrossel categoria="@categoria" produtos="@Model.Produtos" tamanho-pagina="@Model.TamanhoPagina"></vc:carrossel>
    }
</div>
```











































## ViewComponent: Carrossel

*** arquivo: \Areas\Catalogo\Views\Home\Components\Carrossel\Default.cshtml***

```csharp
﻿@using CasaDoCodigo.Areas.Catalogo.Models.ViewModels;
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, CasaDoCodigo.MVC
@model CarrosselViewModel

<h3>@Model.Categoria.Nome</h3>

<div id="my-carousel-@Model.Categoria.Id" class="carousel slide" data-ride="carousel">
    <div class="carousel-inner" role="listbox">
        @{
            for (int pageIndex = 0; pageIndex < Model.NumPaginas; pageIndex++)
            {
                <vc:carrossel-pagina indice-pagina="@pageIndex"
                                     produtos-na-categoria="@Model.Produtos"
                                     tamanho-pagina="@Model.TamanhoPagina"></vc:carrossel-pagina>
            }
        }
    </div>
    <partial name="_NavegacaoCarrossel" model="@Model.Categoria" />
</div>

```

*** arquivo: \Areas\Catalogo\Models\ViewModels\CarrosselViewModel.cs***

```csharp
﻿    public class CarrosselViewModel
{
    public CarrosselViewModel()
    {

    }

    public CarrosselViewModel(Categoria categoria, List<Produto> produtos, int numPaginas, int tamanhoPagina)
    {
        Categoria = categoria;
        Produtos = produtos;
        NumPaginas = numPaginas;
        TamanhoPagina = tamanhoPagina;
    }

    public Categoria Categoria { get; set; }
    public List<Produto> Produtos { get; set; }
    public int NumPaginas { get; set; }
    public int TamanhoPagina { get; set; }
}
```

*** arquivo: \Areas\Catalogo\ViewComponents\CarrosselViewComponent.cs***


```csharp
public class CarrosselViewComponent : ViewComponent
{
    public CarrosselViewComponent()
    {

    }

    public IViewComponentResult Invoke(Categoria categoria, List<Produto> produtos, int tamanhoPagina)
    {
        var produtosNaCategoria = produtos
            .Where(p => p.Categoria.Id == categoria.Id)
            .ToList();
        int pageCount = (int)Math.Ceiling((double)produtosNaCategoria.Count() / tamanhoPagina);

        return View("Default",
            new CarrosselViewModel(categoria, produtosNaCategoria, pageCount, tamanhoPagina));
    }
}
```











## ViewComponent: CarrosselPagina

*** arquivo: \Areas\Catalogo\Views\Home\Components\CarrosselPagina\Default.cshtml***


```csharp
﻿@using CasaDoCodigo.Areas.Catalogo.Models.ViewModels
@addTagHelper *, CasaDoCodigo.MVC
@model CarrosselPaginaViewModel

<div class="item @(Model.IndicePagina == 0 ? "active" : "")">
    <div class="row">
        @{
            foreach (var produto in Model.Produtos)
            {
                <vc:produto-card produto="@produto"></vc:produto-card>
            }
        }
    </div>
</div>
```


*** arquivo: \Areas\Catalogo\Models\ViewModels\CarrosselPaginaViewModel.cs***

```csharp
﻿public class CarrosselPaginaViewModel
{
    public CarrosselPaginaViewModel()
    {

    }

    public CarrosselPaginaViewModel(List<Produto> produtos, int indicePagina)
    {
        Produtos = produtos;
        IndicePagina = indicePagina;
    }

    public List<Produto> Produtos { get; set; }
    public int IndicePagina { get; set; }
}
```

*** arquivo: \Areas\Catalogo\ViewComponents\CarrosselPaginaViewComponent.cs***

```csharp
public class CarrosselPaginaViewComponent : ViewComponent
{
    public CarrosselPaginaViewComponent()
    {

    }

    public IViewComponentResult Invoke(List<Produto> produtosNaCategoria, int indicePagina, int tamanhoPagina)
    {
        var produtosNaPagina =
            produtosNaCategoria
            .Skip(indicePagina * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToList();

        return View("Default",
            new CarrosselPaginaViewModel(produtosNaPagina, indicePagina));
    }
}
```








## ViewComponent: ProdutoCard

*** arquivo: \Areas\Catalogo\Views\Home\Components\ProdutoCard\Default.cshtml***


```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@model CasaDoCodigo.Models.Produto;

@{
    var produto = Model;
}

<div class="col-md-3 col-sm-3 col-lg-3">
    <div class="panel panel-default">
        <div class="panel-body">
            <img class="img-produto-carrossel center-block" src="/images/produtos/large_@(produto.Codigo).jpg" />
        </div>
        <div class="panel-footer produto-footer">
            <div class="produto-nome">@produto.Nome</div>
            <div><h4><strong>R$ @produto.Preco</strong></h4></div>
            <div class="text-center">
                <a asp-area="carrinho" asp-controller="home" asp-action="index"
                   asp-route-codigo="@(produto.Codigo)"
                   class="btn btn-success">Adicionar</a>
            </div>
        </div>
    </div>
</div>
```



*** arquivo: \Areas\Catalogo\ViewComponents\ProdutoCardViewComponent.cs***


```csharp
public class ProdutoCardViewComponent : ViewComponent
{
    public ProdutoCardViewComponent()
    {

    }

    public IViewComponentResult Invoke(Produto produto)
    {
        return View("Default", produto);
    }
}
```




















