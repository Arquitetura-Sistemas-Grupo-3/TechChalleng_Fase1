<h1 align="center">🎮 TechChallenge — Web API de Usuários & Jogos</h1>

<p align="center">
  <strong>FIAP Pós Tech — Arquitetura de Sistemas .NET</strong><br>
  Web API RESTful para gerenciamento de usuários e jogos, com autenticação JWT e controle de acesso por níveis (roles).
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 8" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-Web_API-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="EF Core" />
  <img src="https://img.shields.io/badge/JWT-Auth-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT" />
  <img src="https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger" />
</p>

---

## 📑 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Modelo de Domínio](#-modelo-de-domínio)
- [Segurança](#-segurança)
- [Middlewares Customizados](#-middlewares-customizados)
- [Como Executar](#-como-executar)
- [Usuário Administrador Padrão](#-usuário-administrador-padrão)
- [Autenticação via Swagger](#-autenticação-via-swagger)
- [Testes](#-testes)
- [Estrutura de Pastas](#-estrutura-de-pastas)
- [Autores](#autores)

---

## 📌 Sobre o Projeto

Projeto desenvolvido como parte do **Tech Challenge da Pós Tech FIAP — Arquitetura de Sistemas .NET**.

A aplicação é uma **Web API RESTful** para gerenciamento de **usuários** e **jogos**, com:

- 🔐 Autenticação via **JWT (JSON Web Token)**
- 👥 Controle de acesso baseado em **níveis/roles** (ex.: `Admin`, `Comum`)
- 🧱 Organização em **camadas** seguindo princípios de **Clean Architecture**
- 📖 Documentação interativa via **Swagger / OpenAPI**

---

## 🏛 Arquitetura

O projeto segue uma organização em camadas (**Clean Architecture / Layered Architecture**), separada em quatro projetos dentro da solution `WebAPI.sln`:

| Camada | Projeto | Responsabilidade |
|--------|---------|------------------|
| 🖥 **Apresentação** | `WebAPI` | Controllers, Middlewares, Services de aplicação e configuração da API (`Program.cs`). |
| 🧠 **Domínio** | `Core` | Entidades, Interfaces de Repositório, DTOs de Input/Output e Validações (FluentValidation). |
| 🗄 **Infraestrutura** | `Infra` | `ApplicationDbContext` (EF Core), Migrations, Repositórios e Exceções customizadas. |
| 🧪 **Testes** | `WebAPI.Tests` | Testes automatizados da aplicação. |

```
┌─────────────────────────────────────────────┐
│                   WebAPI                      │  ← Apresentação (Controllers / API)
├─────────────────────────────────────────────┤
│                    Core                       │  ← Domínio (Entidades / Regras)
├─────────────────────────────────────────────┤
│                    Infra                      │  ← Infraestrutura (EF Core / Banco)
└─────────────────────────────────────────────┘
```

---

## 🛠 Tecnologias

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server)
- **Autenticação JWT** (JwtBearer)
- **FluentValidation**
- **Swagger / OpenAPI** (Swashbuckle)
- **xUnit + Moq** (testes automatizados)

---

### 🔑 Autenticação — `AutenticacaoController`

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/Autenticacao?email={email}&senha={senha}` | Realiza login com e-mail e senha, retornando um token **JWT**. |

### 👥 Usuários — `UsuarioController`

| Método | Rota | Descrição | Acesso |
|--------|------|-----------|--------|
| `GET` | `/Usuario` | Lista usuários, com **filtros opcionais** por nome, e-mail e nível de acesso (`?nome=&email=&nivelAcesso=`). | `Admin` |
| `GET` | `/Usuario/{id}` | Busca um usuário pelo ID. | `Admin` |
| `GET` | `/Usuario/me` | Consulta os dados do usuário autenticado (via token JWT). | `Autenticado` |
| `POST` | `/Usuario` | Cadastra um novo usuário com nível de acesso **Usuário**. | `Público` |
| `POST` | `/Usuario/admin` | Cadastra um novo usuário com nível de acesso **Admin**. | `Admin` |
| `PUT` | `/Usuario/{id}` | Atualiza os dados de um usuário existente. | `Autenticado` |
| `DELETE` | `/Usuario/{id}` | Remove (ou inativa) um usuário. | `Admin` |

---

## 🧩 Modelo de Domínio

| Entidade | Descrição |
|----------|-----------|
| **Usuario** | Possui `Nome`, `Email`, `Senha`, `NivelAcesso` (relacionamento) e coleção de `Jogos`. |
| **Jogo** | Possui `Nome` e relacionamento **N:N** com Usuários (via `UsuarioJogo`). |
| **NivelAcesso** | Define os níveis/roles de acesso dos usuários (ex.: `Admin`, `Comum`). |

---

## 🔐 Segurança

- Autenticação baseada em **JWT Bearer Token**.
- As configurações do token (chave e emissor) ficam em `Jwt:Key` e `Jwt:Issuer` no `appsettings.json`.
- Autorização por **políticas**, com a policy `Admin` exigindo a role `Admin` no token.

> ⚠️ **Boa prática:** nunca versione chaves secretas reais no repositório. Utilize *User Secrets*, variáveis de ambiente ou um cofre de segredos em produção.

---

## 🧰 Middlewares Customizados

| Middleware | Função |
|------------|--------|
| **CorrelationIdMiddleware** | Adiciona um Correlation ID único a cada requisição, facilitando a rastreabilidade em logs. |
| **LogMiddleware** | Realiza logging estruturado das requisições/respostas. |
| **ExceptionMiddleware** | Captura exceções não tratadas e retorna respostas padronizadas de erro. |

---

## 🚀 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local, remoto ou container)
- [EF Core Tools](https://learn.microsoft.com/ef/core/cli/dotnet) (`dotnet tool install --global dotnet-ef`), necessário apenas se for aplicar as migrations via linha de comando.

### Passo a passo

**1. Clone o repositório**

```bash
git clone https://github.com/Arquitetura-Sistemas-Grupo-3/TechChalleng_Fase1.git
cd TechChalleng_Fase1
```

**2. Configure a connection string** em `WebAPI/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SEU_SERVIDOR;Database=SEU_BANCO;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

> ℹ️ O banco de dados informado em `Database=` será criado automaticamente ao aplicar as migrations, não é necessário criá-lo manualmente.

**3. Configure as chaves do JWT** em `appsettings.json` (opcional — o repositório já traz valores padrão para ambiente de desenvolvimento)

```json
"Jwt": {
  "Key": "sua-chave-secreta",
  "Issuer": "seu-emissor"
}
```

**4. Aplique as migrations**

Via Visual Studio (Package Manager Console, com o projeto `Infra` selecionado como *Default project*):

```powershell
Update-Database
```

Via CLI, a partir da raiz do repositório:

```bash
dotnet ef database update --project Infra --startup-project WebAPI
```

> As migrations já incluem um *seed* com os níveis de acesso `Admin` e `Usuário`, além de um **usuário administrador padrão** (veja a seção [Usuário Administrador Padrão](#-usuário-administrador-padrão) abaixo) — não é necessário criar um admin manualmente para começar a usar a API.

**5. Execute a aplicação**

Via Visual Studio: defina `WebAPI` como projeto de inicialização e pressione `F5` (ou `Ctrl+F5`).

Via CLI:

```bash
dotnet run --project WebAPI
```

**6. Acesse o Swagger** (ambiente de desenvolvimento)

```
https://localhost:7047/swagger/index.html
```

> A porta pode variar conforme o perfil de execução utilizado (`https`, `http` ou `IIS Express`), definido em `WebAPI/Properties/launchSettings.json`. Ao rodar via `dotnet run`, o console exibirá a URL correta.

---

## 👤 Usuário Administrador Padrão

Para facilitar o primeiro acesso, as migrations já incluem (via `HasData`) um **usuário administrador padrão**, criado automaticamente assim que o `Update-Database` (ou `dotnet ef database update`) é executado:

| Campo | Valor |
|-------|-------|
| **Nome** | `admin` |
| **E-mail** | `admin@gmail.com` |
| **Senha** | `Fiap2026@` |
| **Nível de Acesso** | `Admin` |

Use essas credenciais no endpoint `GET /Autenticacao` para obter um token JWT com permissão de `Admin` e começar a explorar a API imediatamente (por exemplo, para cadastrar outros usuários `Admin` via `POST /Usuario/admin`).

---

## 🔓 Autenticação via Swagger

1. Faça login utilizando o endpoint `GET /Autenticacao`, informando `email` e `senha` (você pode usar o [usuário administrador padrão](#-usuário-administrador-padrão) para o primeiro acesso).
2. Copie o **token JWT** retornado.
3. No Swagger, clique em **Authorize** e informe `Bearer {token}` (ou apenas o token, dependendo da configuração exibida).
4. Utilize o token no header `Authorization: Bearer {token}` para acessar os endpoints protegidos (`Admin` ou autenticado).

---

## 🧪 Testes

O projeto conta com o `WebAPI.Tests`, contendo os testes automatizados da aplicação. Para executá-los:

```bash
dotnet test
```

---

## 📂 Estrutura de Pastas

```
TechChallenge/
├── WebAPI/            # API, Controllers, Middlewares, Program.cs
├── Core/              # Entidades, Interfaces, Inputs/Outputs, Validações
├── Infra/             # DbContext, Migrations, Repositórios e Exceções
└── WebAPI.Tests/      # Testes automatizados
```

---

<a name="autores"></a>
## Autores

| Nome |
|------|
| José Guilherme da Silva Costa |
| Juliana Menezes Hernandes |
| Marcos Vinícius Reis de Souza |
| Murilo dos Santos Cantante |

---
