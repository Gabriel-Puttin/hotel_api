# Boas vindas ao Projeto Hotel API !

Aqui você vai encontrar os detalhes de como foi minha experiência durante o desenvolvimento deste projeto, stacks utilizadas e uma breve documentação sobre como utilizar este projeto em pleno funcionamento.

# Sobre o projeto

Este projeto teve como objetivo contruir uma API de um sistema de booking de várias redes de hotéis em que será possível criar, visualizar, deletar e atualizar(CRUD) cidades, hoteis e apartamentos. Nesse projeto foi utilizada uma arquitetura de software do tipo MVC (model, view e controller), todo o código backend desenvolvido em .Net junto com o ASPNET para enviar requisições e receber as responses, e para fazer a conexão com o banco de dados SQLite (visto que esse projeto foi desenvolvido em ambiente Linux, portando utilizei uma versão opensource do SQL, o SQLite) foi utilizado o framework mais comum entre os desenvolvedores .NET, o Entity Framework.

# Stacks utilizadas

* .NET
* ASPNET
* ASPNET Core
* SQLite
* Entity Framework
* Fluent Assertions
* Docker
* Docker Compose

# Documentação

Para a execução deste propjeto é necessário ter o .Net instalado (linguagem nativa em ambientes windows), o Docker e o Docker-Compose instalados na sua máquina. Portanto confira a documentação oficial para a instalação.

## Instalando Dependências

> Docker

[Link](https://docs.docker.com/engine/install/) para a documentação oficial

> Docker-Compose

[Link](https://docs.docker.com/compose/install/#install-compose) para a documentação oficial

## Executando aplicação

1. Clone o repositório (caso esteja usando chave SSH)
```
git clone git@github.com:Gabriel-Puttin/hotel_api.git
```
2. Entre na pasta que você acabou de clonar
```
cd src
```
3. Instale as dependências do projeto
```
dotnet restore
```
4. Suba o container com o banco de dados com o docker-compose
```
docker-compose up -d
```
5. entre na pasta do projeto
```
cd TrybeHotel
```
6. Suba as migrations para criar o db no banco de dados
```
dotnet ef database update
```
7. Execute a aplicação dentro do diretório TrybeHotel
```
dotnet run
```
8. Be happie (seja feliz :)

# Swagger UI

Como se trata de um projeto desenvolvido em C#, por padrão a biblioteca Swagger já vem pré configurada, e é uma ótima escolha para documentação dos seus projetos!

## Usando o Swagger UI

Para utilizar a libe, basta executar a aplicação, acessar o navegador com o seu localhost que foi instaciado pelo dotnet, como nessa imagem:
<img src="/src/images/print_dotnet_run.png">.
Depois é só acrescentar o "/swagger" ao final da url que você terá uma página semelhante a essa:
<img src="/src/images/print_swagger_ui.png">.
