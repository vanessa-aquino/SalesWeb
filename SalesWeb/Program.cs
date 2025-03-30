using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;

var builder = WebApplication.CreateBuilder(args); // Contrutor da aplicação com as configurações iniciais

// Configuração do Entity:
builder.Services.AddDbContext<SalesWebContext> // Registro do SalesWeb (classe que representa o banco de dados) no sistema de injeção de dependências
(options => options.UseMySql(builder.Configuration.GetConnectionString("SalesWebContext"), // Define o banco como MySQL, pegando a string de conexão do appsettings.json
ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebContext")), // Detecta automáticamente a versão do MySQL a partir da conexão
mySqlOptions => mySqlOptions.MigrationsAssembly("SalesWeb"))); // Indica onde as migrações do banco serão armazenadas

// Add o suporte para MVC, permitindo que a aplicação utilize Controllers e Views
builder.Services.AddControllersWithViews();

var app = builder.Build(); // Finalização da configuração e criação da aplicação a ser executada

// Configuração do Middleware (Conjunto de componentes que manipulkam requisições antes e depois de serem processadas):
if (!app.Environment.IsDevelopment())
{
// Se a aplicação não está rodando em ambiente de desenvolvimento, ativa um manipulador de erros e HSTS(para melhorar a segurança com HTTPS).
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Redireciona todas as requisições HTTP para HTTPS.
app.UseRouting(); // Habilita o roteamento na aplicação

app.UseAuthorization(); // Define que a aplicação usará sistema de autorização (caso tenha autenticação implementada).

app.MapStaticAssets(); // Permite que a aplicação sirva arquivos estáticos como CSS, JavaScript e imagens.

// Definição da rota padrão:
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run(); // Inicia o servidor web para começar a receber requisições.
