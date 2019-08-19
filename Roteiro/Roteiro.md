# ASP.NET Core Parte 5
## Modulariza��o e Componentiza��o

### Introdu��o

A programa��o modular � um dos padr�es de design, que divide os projetos em v�rios m�dulos com base em recursos e considera os m�dulos como projetos separados.

Neste curso abordaremos um Projeto Inicial de uma aplica��o de com�rcio eletr�nico, que passar� por um processo progressivo de modulariza��o e componentiza��o. Iremos aprender a criar �reas, Views Parciais, Componentes Visuais e Isolamento de Contextos e de modelos.

# 1) Areas

### O Projeto Inicial

Para come�ar este curso, trabalharemos com um projeto de loja virtual da editora Casa do C�digo. Esse projeto cont�m as funcionalidades:

1. login/registro de novo usu�rio 
2. cat�logo de produtos
3. carrinho de compras
4. cadastro de usu�rio
5. checkout (fechamento de pedido)

O projeto inicial funciona perfeitamente. Por�m, temos apenas 1 controller para lidar com toda a aplica��o. Al�m disso, todas as views est�o misturadas numa mesma pasta do projeto:

![Antes](antes.png)

Da mesma forma, os modelos tamb�m est�o misturados na mesma pasta Models:

![Modelo Antes](modelo_antes.png)

Claramente, nosso projeto carece de uma boa organiza��o, o que pode dificultar o desenvolvimento de novas funcionalidades. Essa falta de organiza��o dificulta futuras mudan�as e expans�es no projeto, pois n�o existe nenhum m�dulo ou separa��o clara entre as diferentes �reas da aplica��o

Para melhorar a organiza��o do c�digo, o ASP.NET Core possui o conceito de **Area**. �reas s�o um recurso do ASP.NET Core usado para organizar funcionalidades relacionadas em um grupo. 

Vamos demonstrar a cria��o de uma nova �rea em nosso projeto. Essa �rea vai conter as funcionalidades do cat�logo de produtos.

Para criar uma nova �rea, clique com o bot�o direito sobre o nome do projeto, e escolha ***Add > Area***:

![Add Area](add_area.png)

A seguir, preencha o nome da �rea "Catalogo".

![Add Area Catalogo](add_area_catalogo.png)

Pronto, agora temos uma nova subpasta **Catalogo** dentro da pasta **Areas**.

![Catalogo Folder](catalogo_folder.png)



Nova �rea: Catalogo
Nova �rea: Carrinho
Nova �rea: Cadastro




#### Benef�cios da programa��o modular

- Desenvolvimento r�pido
  - Trabalhar em todos os m�dulos simultaneamente por diferentes equipes ou membros reduzir� o tempo de conclus�o.
- A base de c�digo facilmente gerenci�vel
  - Codebase ser� gerenciado sem qualquer aborrecimento.
- Solu��o de problemas f�cil
  - A resolu��o de problemas ser� mais prop�cia, uma vez que � fornecida uma base de c�digo separada para cada m�dulo.
- Responsabilidade definida pelas equipes / membros
  - Cada equipe ou membro ter� uma responsabilidade precisamente predefinida no projeto.


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


