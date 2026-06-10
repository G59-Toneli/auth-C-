# Projeto de Estudo: Sistema de Autenticação com .NET, Entity Framework e PostgreSQL

## Objetivo

Construir uma API REST de autenticação utilizando C#, ASP.NET Core, Entity Framework Core e PostgreSQL.

O foco principal é aprender:

* Entity Framework Core
* DbContext
* Migrations
* Repository Pattern
* Service Layer
* Dependency Injection
* JWT Authentication
* BCrypt
* SOLID
* Clean Architecture (simplificada)
* Boas práticas de APIs REST

Não quero apenas o código pronto. Quero explicações detalhadas de cada conceito, decisão arquitetural e implementação durante o desenvolvimento.

---

## Stack

Backend:

* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* JWT Bearer Authentication
* BCrypt.Net

Ferramentas:

* Swagger/OpenAPI
* EF Core Migrations

---

## Estrutura desejada

Projeto organizado em camadas:

* Controllers
* Services
* Repositories
* Data
* Entities
* DTOs

Exemplo:

AuthApi/
├── Controllers/
├── Services/
├── Repositories/
├── Data/
├── Entities/
├── DTOs/
├── Program.cs
└── appsettings.json

---

## Entidade principal

User

Campos:

* Id (Guid)
* Name
* Email
* PasswordHash
* CreatedAt

---

## Funcionalidades obrigatórias

### 1. Cadastro

Endpoint:

POST /auth/register

Recebe:

{
"name": "Gustavo",
"email": "[gustavo@email.com](mailto:gustavo@email.com)",
"password": "123456"
}

Regras:

* Não permitir email duplicado
* Validar dados de entrada
* Gerar hash da senha com BCrypt
* Salvar usuário no banco

---

### 2. Login

Endpoint:

POST /auth/login

Recebe:

{
"email": "[gustavo@email.com](mailto:gustavo@email.com)",
"password": "123456"
}

Regras:

* Buscar usuário por email
* Validar senha usando BCrypt
* Gerar JWT
* Retornar token

---

### 3. Usuário autenticado

Endpoint:

GET /auth/me

Requer JWT válido.

Retorna:

{
"id": "...",
"name": "...",
"email": "..."
}

Objetivo:

Aprender autenticação baseada em JWT e uso de [Authorize].

---

## Banco de dados

Utilizar PostgreSQL.

Aprender:

* Configuração da Connection String
* DbContext
* DbSet
* Migrations
* Criação automática das tabelas

Quero entender exatamente o que acontece quando executo:

dotnet ef migrations add InitialCreate

e

dotnet ef database update

---

## Repository Pattern

Criar interface:

IUserRepository

Implementação:

UserRepository

Objetivo:

Entender abstração do acesso a dados e aplicação do princípio Dependency Inversion (SOLID).

---

## Service Layer

Criar:

AuthService

Responsabilidades:

* Cadastro
* Login
* Geração de JWT
* Regras de negócio

Objetivo:

Separar regras de negócio do Controller.

---

## Dependency Injection

Configurar todas as dependências no Program.cs.

Quero entender:

* O que é IoC
* O que é DI
* Como o container do .NET resolve dependências
* Ciclos de vida (Scoped, Singleton e Transient)

---

## Entity Framework

Durante o projeto quero aprender:

* DbContext
* DbSet
* LINQ
* FirstOrDefaultAsync
* AddAsync
* SaveChangesAsync
* Include
* Tracking
* NoTracking

Sempre que algum desses conceitos aparecer, explique detalhadamente.

---

## SOLID

Durante a implementação, explique onde os princípios SOLID estão sendo aplicados.

Principalmente:

* Single Responsibility Principle
* Open/Closed Principle
* Dependency Inversion Principle

Com exemplos práticos do próprio projeto.

---

## Extras (se houver tempo)

Após finalizar o auth básico:

### Refresh Token

Implementar:

POST /auth/refresh

Objetivo:

Entender como sistemas reais renovam sessões.

### Roles

Adicionar:

* Admin
* User

Criar endpoint protegido apenas para Admin.

Objetivo:

Aprender Authorization além da Authentication.

---

## Forma de ensino

Atue como um Tech Lead.

Não entregue tudo pronto.

Construa passo a passo.

Explique:

* O que estamos fazendo
* Por que estamos fazendo
* Quais alternativas existem
* Quais perguntas de entrevista podem surgir a partir daquele conceito

O objetivo principal é preparação para entrevistas de desenvolvedor .NET Pleno.
