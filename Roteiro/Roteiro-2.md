# 2) Partial Views

## Vídeo 2.1: Apresentando Partial Views

Nossa aplicação está funcionando perfeitamente, porém algumas das nossas views ficaram enormes, dificultando a leitura e compreensão por parte dos desenvolvedores. Além disso, existem vários trechos duplicados de conteúdo dentro das nossas views.

Podemos otimizar nosso código razor dentro das views, extraindo trechos parciais para arquivos separados. Esses arquivos menores podem ser então referenciados dentro dos arquivos de origem. Esses arquivos de trecho são chamados de Views Parciais, ou Partial Views.
 
Vamos dar uma olhada no arquivo da view de carrinho:

Aqui, podemos ver como o trecho abaixo, com os link para Adicionar Produtos e Preencher Cadastro, está sendo repetido 2 vezes:

**arquivo: \Areas\Carrinho\Views\Home\Index.cshtml**

```razor
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" asp-area="catalogo">
                Adicionar Produtos
            </a>
            <a class="btn btn-success" asp-area="cadastro">
                Preencher Cadastro
            </a>
        </div>
    </div>
</div>
```

Para evitar essa duplicação, você pode extrair esse trecho e copiá-lo em um novo arquivo razor, que fará o papel de **view parcial**. Por padrão, o nome do arquivo de view parcial deve começar com um caracter de sublinhado ("_"):

**arquivo: \Areas\Carrinho\Views\Home\_NavegacaoCarrinho.cshtml**

```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" asp-area="catalogo">
                Adicionar Produtos
            </a>
            <a class="btn btn-success" asp-area="cadastro">
                Preencher Cadastro
            </a>
        </div>
    </div>
</div>
```

No lugar em que esse trecho foi removido do arquivo razor original, devemos adicionar uma tag helper para indicar que nossa nova view parcial deve ser renderizada naquele local.

Isso pode ser feito através da `tag helper <partial>`, indicando o nome da view parcial:

```razor
<partial name="_NavegacaoCarrinho" />
```

...ou através de uma `html helper`:

```razor
@await Html.PartialAsync("_NavegacaoCarrinho")
```














## Vídeo 2.2: Criando outras Partial Views mais Simples

**arquivo: \Areas\Pedido\Views\Home\Index.cshtml**
```razor
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" asp-area="catalogo">
                Voltar ao Catálogo
            </a>
        </div>
    </div>
</div>
```

```razor
<partial name="_Navegacao" />
```


**arquivo: \Areas\Pedido\Views\Home\_Navegacao.cshtml**

```razor
@@ -0,0 +1,10 @@
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" asp-area="catalogo">
                Voltar ao Catálogo
            </a>
        </div>
    </div>
</div>
```


**arquivo: \Areas\Cadastro\Views\Home\Index.cshtml**

antes:
```razor
<div class="form-group">
    <a class="btn btn-success" asp-area="catalogo">
        Continuar Comprando
    </a>
</div>
<div class="form-group">
    <button type="submit"
            class="btn btn-success">
        Finalizar Pedido
    </button>
</div>
```

depois:
```razor
<partial name="_Navegacao" />
```

**arquivo: \Areas\Cadastro\Views\Home\_Navegacao.cshtml**

```razor
﻿<div class="form-group">
    <a class="btn btn-success" asp-area="catalogo">
        Continuar Comprando
    </a>
</div>
<div class="form-group">
    <button type="submit"
            class="btn btn-success">
        Finalizar Pedido
    </button>
</div>
```


#### Resumindo: Declarando views parciais

Uma partial view é um arquivo de marcação .cshtml mantido dentro da pasta Views (MVC) ou Páginas (Razor Pages).

No ASP.NET Core MVC, um ViewResult do controller é capaz de retornar uma view ou uma partial view. 

Ao contrário da view do MVC ou renderização de página, uma partial view não executa _ViewStart.cshtml. Para obter mais informações sobre _ViewStart.cshtml, consulte Layout no ASP.NET Core.

Nomes de arquivos de partial view geralmente começam com um sublinhado (_). Essa convenção de nomenclatura não é obrigatória, mas ajuda a diferenciar visualmente as views parciais das views e das páginas.






















## Vídeo 2.3: Criando Partial Views mais Complexas


**arquivo: \Areas\Catalogo\Views\Home\Index.cshtml**


```razor
<div class="container">
    <div class="row">
        <div class="col-md-4">
            <h2>Buscar produtos</h2>
            <div id="custom-search-input">
                <div class="input-group col-md-12">
                    <form asp-area="Catalogo"                                               
                          asp-controller="Home" 
                          asp-action="Index">
                        <input type="text" asp-for="@Model.Pesquisa" name="pesquisa"
                               class="form-control input-lg"
                               placeholder="categoria ou produto" />
                        <span class="input-group-btn">
                            <button class="btn btn-info btn-lg" type="submit">
                                <i class="glyphicon glyphicon-search"></i>
                            </button>
                        </span>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
```

```razor
<partial name="_FormularioBusca" />
```


**arquivo: \Areas\Catalogo\Views\Home\_FormularioBusca.cshtml**

```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using  CasaDoCodigo.Models.ViewModels;
@model BuscaProdutosViewModel;
<div class="container">
    <div class="row">
        <div class="col-md-4">
            <h2>Buscar produtos</h2>
            <div id="custom-search-input">
                <div class="input-group col-md-12">
                    <form asp-area="Catalogo"
                          asp-controller="Home"
                          asp-action="Index">
                        <input type="text" asp-for="@Model.Pesquisa" name="pesquisa"
                               class="form-control input-lg"
                               placeholder="categoria ou produto" />
                        <span class="input-group-btn">
                            <button class="btn btn-info btn-lg" type="submit">
                                <i class="glyphicon glyphicon-search"></i>
                            </button>
                        </span>
                    </form>
                </div>
            </div>
        </div>
    </div>
</div>
```











```razor
<div class="col-md-3 col-sm-3 col-lg-3">
    <div class="panel panel-default">
        <div class="panel-body">
            <img class="img-produto-carrossel center-block" src="/images/produtos/large_@(produto.Codigo).jpg" />
        </div>
        <div class="panel-footer produto-footer">
            <div class="produto-nome">@produto.Nome</div>
            <div><h4><strong>R$ @produto.Preco</strong></h4></div>
            <div class="text-center">
                <a asp-area="carrinho"                  
                    asp-action="index"
                    asp-route-codigo="@produto.Codigo"
                    class="btn btn-success">Adicionar</a>
            </div>
        </div>
    </div>
</div>
```

```razor
<partial name="_ProdutoCard" model="@produto" />
```

**arquivo: \Areas\Catalogo\Views\Home\_ProdutoCard.cshtml**
```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using CasaDoCodigo.Models;
@model Produto;

<div class="col-md-3 col-sm-3 col-lg-3">
    <div class="panel panel-default">
        <div class="panel-body">
            <img class="img-produto-carrossel center-block" src="/images/produtos/large_@(Model.Codigo).jpg" />
        </div>
        <div class="panel-footer produto-footer">
            <div class="produto-nome">@Model.Nome</div>
            <div><h4><strong>R$ @Model.Preco</strong></h4></div>
            <div class="text-center">
                <a asp-area="carrinho"
                   asp-action="index"
                   asp-route-codigo="@Model.Codigo"
                   class="btn btn-success">Adicionar</a>
            </div>
        </div>
    </div>
</div>
```

#### Resumindo: Quando usar views parciais?

Views parciais são uma maneira eficaz de:

- **Dividir arquivos de marcação grandes em componentes menores.**

	Aproveitar a vantagem de trabalhar com cada parte isolada em uma partial view em um arquivo de marcação grande e complexo, composto por diversas partes lógicas. O código no arquivo de marcação é gerenciável porque a marcação contém somente a estrutura de página geral e as referências a views parciais.

- **Reduzir a duplicação de conteúdo de marcação comum em arquivos de marcação.**

	Quando os mesmos elementos de marcação são usados nos arquivos de marcação, uma partial view remove a duplicação de conteúdo de marcação em um arquivo de partial view. Quando a marcação é alterada na partial view, ela atualiza a saída renderizada dos arquivos de marcação que usam a partial view.

As views parciais não podem ser usadas para manter os elementos de layout comuns. Os elementos de layout comuns precisam ser especificados nos arquivos _Layout.cshtml.

Não use uma partial view em que a lógica de renderização complexa ou a execução de código são necessárias para renderizar a marcação. Em vez de uma partial view, use um **view component**.































