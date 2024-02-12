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
git clone git@github.com:Gabriel-Puttin/Store_Manager.git
```
2. Entre na pasta que você acabou de clonar
```
cd Store_Manager
```
3. Suba a aplicação com o docker-compose
```
docker-compose up -d
```
4. Acesse o terminal do container backend criado
```
docker exec -it store_manager bash
```
5. Instale as dependências
```
npm install
```
6. Crie e gere o banco de dados no container
```
npm run migration && npm run seed
```
7. Rode a aplicação
```
npm start
```
# Swagger UI
