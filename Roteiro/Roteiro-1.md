# ASP.NET Core Parte 5
## Modulariza��o e Componentiza��o

### Introdu��o

A programa��o modular � um dos padr�es de design, que divide os projetos em v�rios m�dulos com base em recursos e considera os m�dulos como projetos separados.

Neste curso abordaremos um Projeto Inicial de uma aplica��o de com�rcio eletr�nico, que passar� por um processo progressivo de modulariza��o e componentiza��o. Iremos aprender a criar �reas, Views Parciais, Componentes Visuais e Isolamento de Contextos e de modelos.

# 1) Areas

## V�deo 1.1: Modulariza��o com ASP.NET Core

#### O Projeto Inicial

Para come�ar este curso, trabalharemos com um projeto de loja virtual da editora Casa do C�digo. Esse projeto cont�m as funcionalidades:

1. login/registro de novo usu�rio 
2. cat�logo de produtos
3. carrinho de compras
4. cadastro de usu�rio
5. checkout (fechamento de pedido)

O projeto inicial funciona perfeitamente. Por�m, quando olhamos a estrutura do projeto, notamos apenas 1 controller para lidar com toda a aplica��o. Al�m disso, todas as views est�o misturadas numa mesma pasta do projeto:

![Antes](antes.png)

Da mesma forma, os modelos tamb�m est�o misturados na mesma pasta Models:

![Modelo Antes](modelo_antes.png)

Claramente, nosso projeto carece de uma boa organiza��o, e sem ela pode ser mais dif�cil desenvolver novas funcionalidades. Essa falta de organiza��o dificulta futuras mudan�as e expans�es no projeto, pois n�o existe nenhum m�dulo ou separa��o clara entre as diferentes �reas da aplica��o

#### Nova �rea: Catalogo

Para melhorar a organiza��o do c�digo, o ASP.NET Core possui o conceito de **Area**. �reas s�o um recurso do ASP.NET Core usado para organizar funcionalidades relacionadas em um grupo. 
Vamos demonstrar a cria��o de uma nova �rea em nosso projeto atrav�s de um processo de ***scaffolding***. Essa �rea vai conter as funcionalidades do cat�logo de produtos.

Para criar uma nova �rea, clique com o bot�o direito sobre o nome do projeto, e escolha ***Add > Area***:

![Add Area](add_area.png)

A seguir, preencha o nome da �rea "Catalogo".

![Add Area Catalogo](add_area_catalogo.png)

Pronto, agora temos uma nova subpasta **Catalogo** dentro da pasta **Areas**.

![Catalogo Folder](catalogo_folder.png)

Agora temos novas pastas no projeto, que podem ser usadas na organiza��o e modulariza��o da aplica��o:

- Areas
	- Catalogo
     	- Controllers
       	- Data
       	- Models
       	- Views


#### Benef�cios da programa��o modular

Voc� pode ter percebido que essas pastas sugerem que os controllers, modelos, views e dados de cat�logo sejam mantidos de forma isolada em rela��o ao resto da aplica��o. Vamos fazer essas mudan�as progressivamente ao longo deste curso.

Vejamos os benef�cios da programa��o modular:

- Desenvolvimento r�pido
  - Trabalhar em todos os m�dulos simultaneamente por diferentes equipes ou membros reduzir� o tempo de conclus�o.
- A base de c�digo facilmente gerenci�vel
  - Codebase ser� gerenciado sem qualquer aborrecimento.
- Solu��o de problemas f�cil
  - A resolu��o de problemas ser� mais prop�cia, uma vez que � fornecida uma base de c�digo separada para cada m�dulo.
- Responsabilidade definida pelas equipes / membros
  - Cada equipe ou membro ter� uma responsabilidade precisamente predefinida no projeto.

## V�deo 1.2 Controller e Roteamento de �rea

A �rea Catalogo conter� modelos, views e controllers espec�ficos do cat�logo de produtos da aplica��o.

Vamos come�ar criando um novo controller dentro da area de Catalogo:

![Home Controller](HomeController.png)

A seguir, copiamos o m�todo `BuscaProdutos()` da classe `PedidoController` para a nova classe `HomeController`, e o renomeamos como `Index()`:

![Busca Produtos Para Home Controller](BuscaProdutos_para_HomeController.png)


```csharp
[Area("Catalogo")]
public class HomeController : Controller
{
    // GET: /<controller>/
    public async Task<IActionResult> Index(string pesquisa)
    {
        return View(await produtoRepository.GetProdutosAsync(pesquisa));
    }
}
```

Por�m, o novo m�todo `Index` acima depende de `produtoRepository`, que n�o existe na classe. Vamos fornec�-lo por inje��o de depend�ncia, que ser� armazenado em um campo privado no construtor da classe:


```csharp
public class HomeController : Controller
{
    private readonly IProdutoRepository produtoRepository;

    public HomeController(IProdutoRepository produtoRepository)
    {
        this.produtoRepository = produtoRepository;
    }
.
.
.
}
```

Agora precisamos definir o `HomeController` como destino de uma nova **rota de �rea**. Ou seja, sempre que o usu�rio navegar para **localhost:5101/Catalogo**, ele dever� acessar a action `Index` de `HomeController` dentro da pasta **Areas/Catalogo**. 

Faremos isso 1) decorando o controller com um atributo e tamb�m 2) definindo uma rota de �rea em **Startup.cs**.

1) Vamos decorar o controller com um atributo AreaAttribute. Esse atributo permite que o HomeController esteja acess�vel para a rota de �rea de Catalogo.

```csharp
[Area("Catalogo")]
public class HomeController : Controller
```

2) Vamos Adicionar uma rota de �rea em **Startup.cs** com o m�todo `MapAreaRoute`:

**Startup.cs (c�digo original)**
```csharp
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Pedido}/{action=BuscaProdutos}/{codigo?}");
});
```

O c�digo a seguir usa MapAreaRoute para criar uma rota de �rea nomeadas:

**Startup.cs (c�digo alterado)**
```csharp
app.UseMvc(routes =>
{
	//criando o mapeamento de rota para a �rea de cat�logo
    routes.MapAreaRoute(
        name: "AreaCatalogo",
        areaName: "Catalogo",
        template: "Catalogo/{controller=Home}/{action=Index}/{pesquisa?}");

    routes.MapRoute(
        name: "default",
        template: "{controller=Pedido}/{action=BuscaProdutos}/{codigo?}");
});
```

O m�todo `MapAreaRoute` acima adiciona uma rota ao `IRouteBuilder` com a �rea MVC especificada com o nome, areaName e template especificados.

Gra�as a esse roteamento de �rea de cat�logo, sempre que o usu�rio navegar para **localhost:5101/Catalogo**, ele ir� acessar a action `Index` de `HomeController` dentro da pasta **Areas/Catalogo**.

Como j� movemos o m�todo `BuscaProdutos()` para o novo controller `HomeController` (renomeado como `Index()`), iremos remover o m�todo original de `PedidoController`:

***Arquivo: PedidoController.cs***

**Remover o m�todo:**
```csharp
public async Task<IActionResult> BuscaProdutos(string pesquisa)
{
    return View(await produtoRepository.GetProdutosAsync(pesquisa));
}
```

Por outro lado, precisamos garantir que, sempre que o usu�rio acesse a rota default **localhost:5101/** (sem �rea), devemos redirecionar o usu�rio para a tela de busca de produtos, isto �, para o endere�o **localhost:5101/Catalogo/Home/Index**. Vamos implementar o `HomeController` na pasta default **Controllers**: 

***Arquivo: \Controllers\HomeController.cs***

```csharp
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Redirect("/Catalogo");
        }
    }
```

O m�todo Redirect acima cria um objeto `RedirectResult` que redireciona para a URL "/Catalogo" da �rea de cat�logo, usando o m�todo HTTP 302 (redirect).

![Url Catalogo](url_Catalogo.png)

### V�deo 1.3 Corre��o de links para nova �rea de cat�logo

Como estamos realizando mudan�as na estrutura de pastas do projeto e no mecanismo de roteamento da aplica��o MVC, isso afetar� os links e endere�os preexistentes em nosso website.

Precisamos identificar os pontos afetados e corrigir os endere�amentos de rota para que a aplica��o continue funcionando normalmente.

Que pontos s�o esses?

Em primeiro lugar, vamos corrigir o link para a home page do website. Esse link est� localizado no logotipo da Casa do C�digo, mais precisamente no arquivo _Layout.cshtml:

***arquivo: Views/Shared/_Layout.cshtml***

```razor
<a asp-controller="Pedido" asp-action="BuscaProdutos" class="navbar-brand"></a>
```

Aqui, vamos remover os atributos asp-controller e asp-action, adicionando o atributo asp-area com o nome da �rea catalogo:

```razor
<a asp-area="catalogo" class="navbar-brand"></a>
```

Mas e quanto aos atributos asp-controller e asp-action? Como eles n�o s�o mencionados, s�o assumidos seus valores default: `Home` e `Index`, respectivamente. No final, esse link ir� direcionar o usu�rio para o endere�o **localhost:5101/cadastro/home/index**, que devolve a p�gina de busca de produtos. 


Seguindo a mesma l�gica, vamos corrigir o link da *partial view* **Views/Shared/_LoginPartial.cshtml**:

**antes:**

<form asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="@Url.Page("/Index", new { area = "" })" method="post" id="logoutForm" class="navbar-right">

**depois:**

<form asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="@Url.Page("/", new { area = "Catalog" })" method="post" id="logoutForm" class="navbar-right"> 

Note que o m�todo `Url.Page` acima agora referencia a �rea Catalogo.

As pr�ximas altera��es corrigem a view de busca de produtos (que chamamos de /Catalogo/Home/Index.cshtml):

***arquivo \Catalogo\Views\Home\Index.cshtml***

Vamos adicionar a diretiva `addTagHelper`, que habilita o uso de tag helpers no c�digo razor. Vamos tamb�m adicionar o namespace do viewmodel `BuscaProdutosViewModel` que � usado pela view como modelo:

```Razor
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@using CasaDoCodigo.Models.ViewModels;
```

Continuando na mesma view, trocamos o atributo asp-action por um atributo asp-area apontando para a �rea de cat�logo:

**antes:**

```Razor
<form asp-action="buscaprodutos">
```

**depois**

```Razor
<form asp-area="Catalogo">
```

Ainda falta corrigir um link na view de cat�logo: aqui, a action de carrinho n�o tem nenhuma indica��o de �rea. Por isso, precisamos dizer que essa action est� numa �rea "vazia", e no controller `PedidoController`:

**antes**
```razor
	<a asp-action="carrinho"
```

**depois**
```razor
	<a asp-area=""                  
	asp-controller="pedido"
	asp-action="carrinho"
```

A seguir, as outras views tamb�m precisam apontar o link para o cat�logo mencionando o atributo de �rea. 

Vamos fazer essa altera��o nas views:

- **Cadastro**
- **Carrinho**
- **Resumo**

***arquivo Views\Cadastro.cshtml***

**antes**

```razor
<a class="btn btn-success" asp-action="buscaprodutos">
```

**depois**

```razor
<a class="btn btn-success" asp-area="Catalogo">
```



***arquivo Views\Carrinho.cshtml***
**antes**
```razor
<a class="btn btn-success" asp-action="buscaprodutos">
```

**depois**
```razor
<a class="btn btn-success" asp-area="Catalogo">
```

**antes**
```razor
<a class="btn btn-success" asp-action="buscaprodutos">
```

**depois**
```razor
<a class="btn btn-success" asp-area="Catalogo">
```

***arquivo Views\Resumo.cshtml***

**antes**
```razor
<a class="btn btn-success" asp-action="buscaprodutos">
```
**depois**
```razor
<a class="btn btn-success" asp-area="Catalogo">
```

Com essas altera��es finais, nossa aplica��o voltou a funcionar normalmente como antes. A diferen�a � que conseguimos organizar e modularizar a �rea de Cat�logo. Voc� tamb�m pode criar as outras �reas do projeto dessa forma (ex.: Carrinho, Cadastro, Resumo, Pedido), deixando a aplica��o organizada por grupo de funcionalidades.



















### Nova �rea: Carrinho
### Nova �rea: Cadastro






### Partial Views

Partial views
More Partial Views

### View Components

View Components

### Isolando Contextos

Isolando Contextos

### Modelos Delimitados

Modelos Delimitados

P�s-v�deo: removendo produto e categoria do contexto principal da apl� 

### Conclus�o


