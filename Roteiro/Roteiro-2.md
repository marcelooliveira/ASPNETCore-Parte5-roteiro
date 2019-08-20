# 2) Partial Views

Nossa aplicação está funcionando perfeitamente, porém algumas das nossas views ficaram enormes, dificultando a leitura e compreensão por parte dos desenvolvedores. Além disso, existem vários trechos duplicados de conteúdo dentro das nossas views.

Podemos otimizar nosso código razor dentro das views, extraindo trechos parciais para arquivos separados. Esses arquivos menores podem ser então referenciados dentro dos arquivos de origem. Esses arquivos de trecho são chamados de Views Parciais, ou Partial Views.



Vamos dar uma olhada no arquivo 




\Areas\Carrinho\Views\Home\Index.cshtml

antes:
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" href="/">
                Adicionar Produtos
            </a>
            <a class="btn btn-success" asp-area="cadastro">
                Preencher Cadastro
            </a>
        </div>
    </div>
</div>

depois:
<partial name="_NavegacaoCarrinho" />



\Areas\Carrinho\Views\Home\_NavegacaoCarrinho.cshtml

@@ -0,0 +1,13 @@
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" href="/">
                Adicionar Produtos
            </a>
            <a class="btn btn-success" asp-area="cadastro">
                Preencher Cadastro
            </a>
        </div>
    </div>
</div>












\Areas\Pedido\Views\Home\Index.cshtml
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" href="/">
                Voltar ao Catálogo
            </a>
        </div>
    </div>
</div>
<partial name="_Navegacao" />


\Areas\Pedido\Views\Home\_Navegacao.cshtml
@@ -0,0 +1,10 @@
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
<div class="row">
    <div class="col-md-12">
        <div class="pull-right">
            <a class="btn btn-success" href="/">
                Voltar ao Catálogo
            </a>
        </div>
    </div>
</div>











\Areas\Cadastro\Views\Home\Index.cshtml

antes:
<div class="form-group">
    <a class="btn btn-success" href="/">
        Continuar Comprando
    </a>
</div>
<div class="form-group">
    <button type="submit"
            class="btn btn-success">
        Finalizar Pedido
    </button>
</div>

depois:
<partial name="_Navegacao" />



\Areas\Cadastro\Views\Home\_Navegacao.cshtml

﻿<div class="form-group">
    <a class="btn btn-success" href="/">
        Continuar Comprando
    </a>
</div>
<div class="form-group">
    <button type="submit"
            class="btn btn-success">
        Finalizar Pedido
    </button>
</div>






\Areas\Catalogo\Views\Home\Index.cshtml


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
<partial name="_FormularioBusca" />



\Areas\Catalogo\Views\Home\_FormularioBusca.cshtml

@@ -0,0 +1,26 @@
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
<partial name="_ProdutoCard" model="@produto" />

\Areas\Catalogo\Views\Home\_ProdutoCard.cshtml
@@ -0,0 +1,21 @@
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
































Uma partial view é um arquivo Razor ( .cshtml) que renderiza um fragmento de html dentro de outro arquivo razor ou html.

O termo partial view é usado durante o desenvolvimento de um aplicativo MVC, no qual os arquivos de marcação são chamados de views, ou de um aplicativo Razor Pages, no qual os arquivos de marcação são chamados de páginas.

#### Quando usar views parciais?

Views parciais são uma maneira eficaz de:

- **Dividir arquivos de marcação grandes em componentes menores.**

	Aproveitar a vantagem de trabalhar com cada parte isolada em uma partial view em um arquivo de marcação grande e complexo, composto por diversas partes lógicas. O código no arquivo de marcação é gerenciável porque a marcação contém somente a estrutura de página geral e as referências a views parciais.

- **Reduzir a duplicação de conteúdo de marcação comum em arquivos de marcação.**

	Quando os mesmos elementos de marcação são usados nos arquivos de marcação, uma partial view remove a duplicação de conteúdo de marcação em um arquivo de partial view. Quando a marcação é alterada na partial view, ela atualiza a saída renderizada dos arquivos de marcação que usam a partial view.

As views parciais não podem ser usadas para manter os elementos de layout comuns. Os elementos de layout comuns precisam ser especificados nos arquivos _Layout.cshtml.

Não use uma partial view em que a lógica de renderização complexa ou a execução de código são necessárias para renderizar a marcação. Em vez de uma partial view, use um **view component**.

#### Declarando views parciais

Uma partial view é um arquivo de marcação .cshtml mantido dentro da pasta Views (MVC) ou Páginas (Razor Pages).

No ASP.NET Core MVC, um ViewResult do controller é capaz de retornar uma view ou uma partial view. No Razor Pages, um PageModel pode retornar uma partial view, representada como um objeto PartialViewResult. A referência e a renderização de views parciais são descritas na seção Referenciar uma partial view.

Ao contrário da view do MVC ou renderização de página, uma partial view não executa _ViewStart.cshtml. Para obter mais informações sobre _ViewStart.cshtml, consulte Layout no ASP.NET Core.

Nomes de arquivos de partial view geralmente começam com um sublinhado (_). Essa convenção de nomenclatura não é obrigatória, mas ajuda a diferenciar visualmente as views parciais das views e das páginas.

