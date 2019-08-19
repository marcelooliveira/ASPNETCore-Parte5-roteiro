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

## V�deo 1.2 �reas

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
[Area("Catalogo")]
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

Gra�as ao roteamento de �rea de cat�logo, agora, sempre que o usu�rio navegar para **localhost:5101/Catalogo**, ele ir� acessar a action `Index` de `HomeController` dentro da pasta **Areas/Catalogo**.





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


