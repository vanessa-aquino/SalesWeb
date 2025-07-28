# SalesWeb MVC

Projeto ASP.NET MVC para gestão de vendas e vendedores. Um sistema completo com funcionalidades de CRUD para cadastro, edição e acompanhamento de vendedores, departamentos e registros de vendas.

![Badge](https://img.shields.io/badge/ASP.NET-MVC-purple)
![Badge](https://img.shields.io/badge/Entity%20Framework-Core-purple)
![Badge](https://img.shields.io/badge/Testes-Automatizados-purple)

## 💡 Sobre o projeto

O **SalesWeb MVC** foi desenvolvido com o objetivo de praticar os principais conceitos do ASP.NET MVC, Entity Framework e padrão de desenvolvimento em camadas. Ele simula um sistema de gestão comercial, onde é possível:

- Cadastrar vendedores e seus respectivos departamentos.
- Registrar vendas com data, status e valor.
- Editar, remover e visualizar todos os dados.
- Filtrar vendas por data.
- Validar dados via Data Annotations.


## 🚀 Tecnologias utilizadas

- [ASP.NET Core MVC](https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-8.0)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)
- MySQL (via Entity Framework)

## 🧱 Estrutura do projeto
SalesWeb/

├── Areas/Identity          # Gerenciamento de login e autenticação

├── Controllers/            # Lógica dos endpoints MVC

├── Data/                   # DbContext e seed de dados

├── Extensions/             # Métodos utilitários e helpers

├── Migrations/             # Migrations do EF Core

├── Models/                 # Entidades de domínio

├── Properties/             # Configurações do projeto

├── Services/               # Camada de regras de negócio

├── ViewModels/             # Objetos de transferência para as views

├── Views/                  # Arquivos Razor (páginas HTML com C#)

├── wwwroot/                # Arquivos estáticos (CSS, JS, img)

├── Program.cs              # Configuração do pipeline do ASP.NET

├── SalesWeb.csproj         # Arquivo do projeto .NET

└── appsettings*.json       # Configurações da aplicação

## 🧪 Como rodar o projeto localmente

Siga os passos abaixo para executar o projeto SalesWeb MVC em sua máquina:

### ✅ Pré-requisitos

Antes de tudo, certifique-se de ter instalado:

- [.NET SDK 8.0 ou superior](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022 ou superior](https://visualstudio.microsoft.com/pt-br/)
  - Com o **workload "ASP.NET e desenvolvimento web"**
- Um servidor de banco de dados (ex: MySQL ou SQL Server)
- Ferramenta de gerenciamento de pacotes (NuGet) já vem no VS

### 🔄 Passo a passo

1. **Clone o repositório**
   ```bash
   git clone https://github.com/vanessa-aquino/SalesWeb.git
   cd SalesWeb
2. **Abra o projeto no Visual Studio**

   - Dê duplo clique no arquivo `SalesWeb.sln`
   - Aguarde enquanto o Visual Studio restaura as dependências

3. **Configure a string de conexão**

   No arquivo `appsettings.json`, edite a seção `ConnectionStrings` com os dados do seu banco de dados. Exemplo para MySQL:

   ```json
   "ConnectionStrings": {
     "SalesWebContext": "server=localhost;userid=root;password=suasenha;database=saleswebapp"
   }

4. **Rode as migrations para criar o banco**

    No terminal integrado do Visual Studio (ou usando o CLI do .NET), execute o     seguinte comando para aplicar as migrations e criar o banco de dados:

    ```bash
    dotnet ef database update

5. **Execute o projeto** 

    Pressione F5 ou clique no botão "Run" no Visual Studio.

    O navegador será aberto automaticamente com o sistema rodando localmente
    (geralmente em https://localhost:xxxx)

## 🤝 Contribuições
Esse projeto foi feito como parte do meu aprendizado, mas se você tiver ideias de melhorias, sugestões ou quiser conversar sobre .NET, é só abrir uma issue ou me chamar! ✨

