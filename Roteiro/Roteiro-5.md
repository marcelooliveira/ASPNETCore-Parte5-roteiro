# 5) Modelos Delimitados

## Vídeo 5.1 - Criando um Novo Contexto

Na última aula, além do banco de dados CasaDoCodigo, passamos a contar também com o novo banco de dados CasaDoCodigo.Catalogo, contendo apenas as tabelas `Produto` e `Categoria`. Essas tabelas fazem parte de ambos os bancos de dados. Esse tipo de duplicação pode parecer ruim, mas na verdade esse tipo de separação permite que que a base de dados da área de Catálogo possa evoluir de forma independente das demais áreas. A ideia é que as modificações no banco de dados do Catalogo não afetem outros bancos de dados.

Nesta aula veremos como isolar também o modelo do catálogo, que funcionará de forma independente dos modelos das outras áreas da aplicação.

