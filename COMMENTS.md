# Arquitetura

## Back-end
Foi utilizada uma estrutura de API utilizando .NET Core 8 e o banco de dados MySQL.
    
Optei por manter a API simples. Apenas uma controller para Alunos foi utilizada, contendo apenas os 5 métodos básicos para criação, consulta (todos ou por ID), remoção e atualização.

Decidi por criar uma paginação *server-side* para a listagem de alunos, assumindo que a lista poderia aumentar significativamente, isso evita problemas de memória e carregamento da página para o futuro.

Considerando que o campo RA foi descrito como não editável, optei por desenvolver um sistema de criação da chave primária de forma automática e sem permitir o *input* do usuário. Utilizei como inspiração o mesmo método utilizado enquanto eu estava na universidade, {ano}{semestre}{matricula}, de maneira simplificada.

O script para criação do banco de dados e tabela, assim como a adição de alguns dados de exemplo está disponível no arquivo `Script_Inicializacao.sql`.

## Front-end
Não possuo tanta familiaridade com Vue.js, portanto tentei manter o projeto enxuto e simples de se utilizar e entender.

Criei o projeto utilizando o comando `npm create vuetify@latest` e apenas modifiquei as rotas e adicionei os componentes necessários: um componente de formulário para alunos que pode ser utilizado para criação ou edição, uma tela inicial simples e uma tela de listagem.

# Lista de Bibliotecas utilizadas
## Back-end
* ***EntityFrameworkCore***
    <p> Biblioteca para conexão com o banco de dados </p>
* ***EntityFrameworkCore.Design***
    <p> Biblioteca para conexão com o banco de dados </p>
* ***EntityFrameworkCore.Tools***
    <p> Biblioteca para conexão com o banco de dados </p>
* ***Pomelo.EntityFrameworkCore.MySql***
    <p> Utilizada para facilitar a conexão com a base de dados MySQL com o Entity Framework</p>
	
## Front-end
* ***axios***
    <p> Biblioteca facilitar as chamadas para a API </p>
* ***vue-sweetalert2***
    <p> Utilizada para a criação da modal de alerta e mensagens de sucesso ou erro </p>

# O que faria com mais tempo?
Existem diversas coisas que eu gostaria de priorizar com mais tempo de desenvolvimento.

Por exemplo, gostaria de ter adicionado um método de autenticação, a fim de demonstrar uma maneira de diferenciar usuários com permissão para remover alunos da base de dados e usuários que podem apenas criar novos.

Também gostaria de ter adicionado os testes unitários.

Por fim gostaria de ter conseguido dedicar mais tempo à estilização do projeto, com uma paleta de cores e estilos mais bem definido além dos já disponíveis do vuetify.