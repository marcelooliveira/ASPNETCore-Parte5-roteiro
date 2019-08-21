# 2) Partial Views

## Vídeo 2.1: Apresentando Partial Views

Nossa aplicação está funcionando perfeitamente, porém algumas das nossas views ficaram enormes, dificultando a leitura e compreensão por parte dos desenvolvedores. Além disso, existem vários trechos duplicados de conteúdo dentro das nossas views. Isso é comum quando você está focado em apenas seguir os requisitos e implementar interface do usuário e regras de negócio para a sua aplicação. Entre uma tarefa e outra, você pode procurar melhorar a organização do projeto, para facilitar futuras implementações.

Podemos otimizar nosso código razor dentro das views, extraindo trechos parciais para arquivos separados. Esses arquivos menores podem ser então referenciados dentro do arquivo de origem, no mesmo local de onde o trecho foi extraído. Esses arquivos de trecho são chamados de Views Parciais, ou Partial Views.
 
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

No lugar de onde esse trecho foi removido do arquivo razor original, devemos adicionar uma tag helper para indicar que nossa nova view parcial deve ser renderizada naquele local.

Isso pode ser feito através da `tag helper <partial>`, indicando o nome da view parcial:

```razor
<partial name="_NavegacaoCarrinho" />
```

...ou através de uma `html helper`:

```razor
@await Html.PartialAsync("_NavegacaoCarrinho")
```

Ambos os métodos renderizam uma partial view de modo assíncrono.

Note que o conteúdo de uma view parcial não é diferente de uma view comum. A diferença é o papel que elas representam na sua aplicação.












## Vídeo 2.2: Criando outras Partial Views mais Simples

Outros arquivos razor (cshtml) da nossa aplicação possuem trechos repetidos, que são ótimos candidatos para se tornarem views parciais.

Na view de de Cadastro, vamos extrair o trecho contendo o botões "Continuar Comprando" e "Finalizar Pedido"...

**arquivo: \Areas\Cadastro\Views\Home\Index.cshtml**

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


... e substituí-los pela view parcial "_Navegacao":

```razor
<partial name="_Navegacao" />
```

O novo arquivo _Navegacao.cshtml ficará localizado na mesma pasta da view de origem:

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

Na view de de pedido, vamos extrair o trecho contendo o botão "Voltar ao Catálogo"...

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

... e substituí-lo pela view parcial "_Navegacao":

```razor
<partial name="_Navegacao" />
```

O novo arquivo _Navegacao.cshtml ficará localizado na mesma pasta da view de origem:

**arquivo: \Areas\Pedido\Views\Home\_Navegacao.cshtml**

```razor
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



#### Resumindo: Declarando views parciais

Uma partial view é um arquivo de marcação .cshtml mantido dentro da pasta Views (MVC) ou Páginas (Razor Pages).

No ASP.NET Core MVC, um ViewResult do controller é capaz de retornar uma view ou uma partial view. 

Ao contrário da view do MVC ou renderização de página, uma partial view não executa _ViewStart.cshtml. 

Nomes de arquivos de partial view geralmente começam com um sublinhado (_). Essa convenção de nomenclatura não é obrigatória, mas ajuda a diferenciar visualmente as views parciais das views e das páginas.






















## Vídeo 2.3: Criando Partial Views mais Complexas

Até agora, vimos como utilizar views parciais somente com conteúdo estático. Mas você pode criar views parciais que renderizam conteúdo dinâmico, tal como uma view normal.

Quando uma view parcial é instanciada, ela recebe uma cópia do dicionário ViewData do pai. As atualizações feitas nos dados dentro da view parcial não afetam a view principal. Alterações a ViewData em uma view parcial são perdidas quando a view parcial é renderizada.
O exemplo a seguir demonstra como passar uma instância de ViewDataDictionary para uma view parcial:

```razor
@await Html.PartialAsync("_NomeDaViewParcial", customViewData)
```

ou você pode usar tag helper, de forma mais simples:

```razor
<partial name="_NomeDaViewParcial"/>
```

Você também pode passar um modelo para uma view parcial. O modelo pode ser um objeto personalizado. Você pode passar um modelo com PartialAsync (renderiza um bloco de conteúdo para o chamador) ou RenderPartialAsync (transmite o conteúdo para a saída):

```razor
@await Html.PartialAsync("_NomeDaViewParcial", model)
```

ou você pode usar tag helper, de forma mais simples:

```razor
<partial name="_NomeDaViewParcial"/>
```



Agora vamos abrir a view de catálogo e extrair o trecho que contém o formulário de busca de produtos:


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

Vamos mover esse trecho para um novo arquivo de view parcial _FormularioBusca.cshtml, que será criado na mesma pasta da view de catálogo:

**arquivo: \Areas\Catalogo\Views\Home\_FormularioBusca.cshtml**

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

Note que essa view parcial depende do modelo, portanto será necessário adicionar 3 linhas no início do novo arquivo para:

1. Adicionar tag helpers com a diretiva `@addTagHelper`
2. Importar o namespace com a diretiva `@using`
3. Declarar o tipo do modelo com a diretiva `@model`

```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using  CasaDoCodigo.Models.ViewModels;
@model BuscaProdutosViewModel;
<div class="container">
.
.
.
```

E no lugar do formulário na view original de catálogo, inserimos a tag helper referenciando a view parcial que foi extraída:

```razor
<partial name="_FormularioBusca" model="@Model" />
```

Podemos simplificar essa declaração omitindo o parâmetro `model`, pois o modelo da view parcial é o mesmo modelo da view principal:


```razor
<partial name="_FormularioBusca" />
```








Por último, continuando ainda na view de catálogo, vamos extrair o trecho que contém o cartão com o detalhe do produto:


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

Vamos mover esse trecho para um novo arquivo de view parcial _ProdutoCard.cshtml, que será criado na mesma pasta da view de catálogo:

**arquivo: \Areas\Catalogo\Views\Home\_ProdutoCard.cshtml**
```razor
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

Note que essa view parcial depende do modelo, portanto será necessário adicionar 3 linhas no início do novo arquivo para:

1. Adicionar tag helpers com a diretiva `@addTagHelper`
2. Importar o namespace com a diretiva `@using`
3. Declarar o tipo do modelo com a diretiva `@model`

```razor
﻿@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using CasaDoCodigo.Models;
@model Produto;
<div class="col-md-3 col-sm-3 col-lg-3">
.
.
.
```



E no lugar do formulário na view original de catálogo, inserimos a tag helper referenciando a view parcial que foi extraída:

```razor
<partial name="_ProdutoCard"/>
```

Porém, o modelo da view parcial é **diferente** do modelo da view principal. Nesse caso, precisamos deixar explícito que o modelo da view parcial é o objeto `produto`:

```razor
<partial name="_ProdutoCard" model="@produto" />
```


#### Resumindo: Quando usar views parciais?

Views parciais são uma maneira eficaz de:

- **Dividir arquivos de marcação grandes em componentes menores.**

	Aproveitar a vantagem de trabalhar com cada parte isolada em uma partial view em um arquivo de marcação grande e complexo, composto por diversas partes lógicas. O código no arquivo de marcação é gerenciável porque a marcação contém somente a estrutura de página geral e as referências a views parciais.

- **Reduzir a duplicação de conteúdo de marcação comum em arquivos de marcação.**

	Quando os mesmos elementos de marcação são usados nos arquivos de marcação, uma partial view remove a duplicação de conteúdo de marcação em um arquivo de partial view. Quando a marcação é alterada na partial view, ela atualiza a saída renderizada dos arquivos de marcação que usam a partial view.

As views parciais não podem ser usadas para manter os elementos de layout comuns. Os elementos de layout comuns precisam ser especificados nos arquivos _Layout.cshtml.

Não use uma partial view em que a lógica de renderização complexa ou a execução de código são necessárias para renderizar a marcação. Nesse caso, em vez de uma partial view, use um **view component**, que apresentaremos na próxima aula. Até lá! 































