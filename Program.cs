using EmporioIrmasDaTerra.Data;
using Microsoft.EntityFrameworkCore;
using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories;

// Cria o "construtor" (builder) da aplicação, que carrega as configurações padrão (ex: appsettings.json) 
// e é usado para registrar os serviços (banco de dados, padrão mvc, repository).
var builder = WebApplication.CreateBuilder(args);

// Registra o AppDbContext no sistema e o configura para usar um banco de dados temporário 
// na memória chamado "EcommerceDb_Teste2".
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EcommerceDb_Teste2"));

// Ativa o padrão MVC. Isso ensina o site a usar "Controllers" para gerenciar as requisições 
// e "Views" para desenhar o HTML.
builder.Services.AddControllersWithViews();

// necessario para session
builder.Services.AddDistributedMemoryCache(); // cache em memoeria para a sessao
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Registra o Padrão de Repositório que separa a lógica de negocio da lógica de dados.
// Quando um controller solicita IProdutoRepository, é entregue um ProdutoRepository (que busca os dados no bd) (injeção de dependência).
// "AddScoped" significa que uma nova instância de ProdutoRepository será criada para cada requisição web (visita no site).
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Registro do Repositório de Usuário para Login/Cadastro em memória
builder.Services.AddScoped<IUsuarioRepository, UsuarioMemoryRepository>();

builder.Services.AddHttpContextAccessor();   // para acessar HttpContext em controllers

// Constrói o site (o "app") usando todos os serviços e configurações que foram registrados no "builder" (o construtor) acima.
var app = builder.Build();

// Inicia um "escopo de serviço" temporário (simulando uma "visita ao site").
// Isso é necessário para usar com segurança o AppDbContext, que é um serviço 'Scoped' (temporário).
// O 'using' garante que este escopo e todos os serviços dentro dele sejam "limpos" (descartados) no final do bloco
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Garante que o banco foi criado
    dbContext.Database.EnsureCreated();

    // 2. CHAMA O MÉTODO DE POPULAR OS DADOS:
    SeedData.Initialize(dbContext);
}

// Redireciona HTTP → HTTPS (se estiver habilitado no projeto)
app.UseHttpsRedirection();

// Habilita o servidor web a "servir" (enviar) arquivos estáticos
// (como CSS, JavaScript e imagens) da pasta 'wwwroot'.
app.UseStaticFiles();

// Ativa o "roteador" do ASP.NET Core, o "recepcionista" que
// analisa a URL da requisição (ex: /Home/Index).
app.UseRouting();

// ATENÇÃO: UseSession deve vir ANTES de Authorization
app.UseSession();

app.UseAuthorization();

// "Distribui" as requisições para os Controllers
app.MapControllerRoute(
    // Define um nome para esta rota padrão.
    name: "default",
    // Define o "padrão" da URL:
    // 1º parte = {controller}, se não tiver, usa 'Home'.
    // 2º parte = {action}, se não tiver, usa 'Index'.
    // 3º parte = {id?}, o '?' indica que este parâmetro é opcional.
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Inicia a aplicação
app.Run();
