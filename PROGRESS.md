# PROGRESS — Sistema de Autenticação (.NET 10 + EF Core + PostgreSQL + JWT)

> Arquivo de continuidade entre máquinas. Atualizado a cada passo.
> Roteiro completo do projeto: `.claude/PROJECT.md`.
> **Regra do projeto:** o Gustavo escreve TODO o código/comandos manualmente; o Claude só guia, explica e revisa.

**📍 VOCÊ ESTÁ AQUI:** Fase 3 — registrar `UserRepository` na DI do `Program.cs` (`AddScoped<IUserRepository, UserRepository>`). Interface e implementação já prontas e compilando. Próximo: Fase 4 (Service + DTOs + BCrypt).

---

## ⚙️ Setup numa máquina nova (rodar antes de continuar)

Quando trocar de PC, garanta o ambiente:

```powershell
# 1. Clonar o repo (se ainda não tiver)
git clone <url-do-repo> ; cd auth

# 2. SDK .NET 10 instalado?  -> https://dotnet.microsoft.com/download
dotnet --version              # esperado: 10.0.x

# 3. Ferramenta de migrations (global, por máquina)
dotnet tool install --global dotnet-ef

# 4. Docker Desktop instalado e RODANDO (ícone da baleia ativo)
winget install Docker.DockerDesktop   # se não tiver
docker --version

# 5. Restaurar pacotes do projeto
dotnet restore

# 6. Subir o banco
docker compose up -d
```

---

## 🔑 Decisões / credenciais (NÃO esquecer — precisam bater na connection string)

| Item | Valor |
|---|---|
| Postgres user | `authuser` *(confirmar no docker-compose.yml)* |
| Postgres password | `authpass` *(confirmar no docker-compose.yml)* |
| Postgres database | `authdb` *(confirmar no docker-compose.yml)* |
| Host / porta | `localhost:5432` |
| Imagem Docker | `postgres:16` |
| Id da entidade User | `Guid`, gerado na app (`Guid.NewGuid()`) |
| Datas | sempre `DateTime.UtcNow` (exigência do Npgsql) |
| Senhas no banco | só hash BCrypt (campo `PasswordHash`) |

---

## ✅ Checklist por fase

### Fase 0 — Ambiente
- [x] .NET SDK 10.0.203 instalado
- [x] `dotnet-ef` 10.0.8 instalado (global)
- [ ] Docker Desktop instalado e rodando
- [x] `.gitignore` para .NET criado na raiz (`bin/`/`obj/` removidos do versionamento)
- [x] `CLAUDE.md` (regras do mentor) versionado — replica o comportamento no 2º PC
- [x] Repo sincronizado no GitHub (`origin/main`)

### Fase 1 — Fundação
- [x] Solution `Auth` criada (`Auth.slnx`)
- [x] Projeto `AuthApi` (Web API com `--use-controllers`) criado e adicionado à solution
- [x] Arquivos de exemplo `WeatherForecast*` removidos
- [x] App sobe com `dotnet run` (http://localhost:5020)

### Fase 2 — Entity + DbContext + PostgreSQL
- [x] `Entities/User.cs` — `Id (Guid)`, `Name`, `Email`, `PasswordHash` (required), `CreatedAt` (UtcNow)
- [x] Pacotes NuGet: `Npgsql.EntityFrameworkCore.PostgreSQL` 10.0.2, `Microsoft.EntityFrameworkCore.Design` 10.0.8
- [x] `Data/AppDbContext.cs` — herda `DbContext`, ctor com `DbContextOptions`, `DbSet<User> Users`
- [x] `docker-compose.yml` na raiz (serviço Postgres)
- [x] Connection string em `appsettings.json`
- [x] Registrar `AppDbContext` na DI no `Program.cs` (`AddDbContext` + `UseNpgsql`)
- [x] `dotnet ef migrations add InitialCreate`
- [x] `dotnet ef database update` (tabela `Users` criada no Postgres)

### Fase 3 — Repository Pattern
- [x] `Repositories/IUserRepository.cs` (interface) — `GetByEmailAsync`, `ExistsByEmailAsync`, `AddAsync`, `SaveChangesAsync`
- [x] `Repositories/UserRepository.cs` (implementação) — DI por construtor, `_context` readonly, build OK
- [ ] Registrar na DI (`AddScoped<IUserRepository, UserRepository>`) **← em andamento**

### Fase 4 — Service Layer + DTOs + BCrypt
- [ ] DTOs: `RegisterRequest`, `LoginRequest`, `AuthResponse`, `UserResponse`
- [ ] Pacote `BCrypt.Net-Next`
- [ ] `Services/IAuthService.cs` + `Services/AuthService.cs` (register/login + regras)
- [ ] Registrar na DI

### Fase 5 — JWT + Controllers
- [ ] Pacote `Microsoft.AspNetCore.Authentication.JwtBearer`
- [ ] Config JWT em `appsettings.json` (Issuer, Audience, Key)
- [ ] Geração de token no `AuthService`
- [ ] `Program.cs`: `AddAuthentication`/`AddJwtBearer` + `UseAuthentication` antes de `UseAuthorization`
- [ ] `Controllers/AuthController.cs`: `POST /auth/register`, `POST /auth/login`, `GET /auth/me [Authorize]`
- [ ] Swagger UI navegável (com suporte a Bearer token)

### Fase 6 — Extras (se houver tempo)
- [ ] Refresh Token (`POST /auth/refresh`)
- [ ] Roles (Admin/User) + endpoint protegido por role

---

## 🗒️ Notas de sessão
- 2026-06-10: Fundação + entity + DbContext prontos. Docker ainda não instalado. Parado na escrita do `docker-compose.yml`.
- 2026-06-10: Fase 2 concluída. docker-compose, connection string, DI, migrations e database update — tabela `Users` criada no Postgres.
- 2026-06-11: Fase 3 — `IUserRepository` + `UserRepository` escritos e compilando. Estudado a fundo: interface vs implementação, async/Task, lambda no EF Core, escopo de parâmetro vs campo, Constructor Injection (DIP). Falta só registrar na DI. Continua de outro PC.
