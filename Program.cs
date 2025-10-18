using EmporioIrmasDaTerra.Data;
using Microsoft.EntityFrameworkCore;
using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories; 

var builder = WebApplication.CreateBuilder(args);

// Adiciona o AppDbContext ao "container de serviços" da aplicação.
// E o configura para usar um banco de dados em memória chamado "EcommerceDb".
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("EcommerceDb_Teste2"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// <-- 2. ADICIONE ESTA LINHA ABAIXO
// Registra o Padrão de Repositório (Injeção de Dependência)
// Diz ao ASP.NET: "Quando um construtor pedir IProdutoRepository, 
// entregue uma nova instância de ProdutoRepository."
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();


var app = builder.Build();

// Este bloco cria um "escopo" para acessar os serviços da aplicação,
// como o AppDbContext, para popular o banco em memória.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    // Pega o contexto do banco de dados
    var context = services.GetRequiredService<AppDbContext>();

    // Garante que o banco em memória foi criado
    context.Database.EnsureCreated();

    // Verifica se já existem dados na tabela de Categorias
    if (!context.Categorias.Any())
    {
        // 1. Cria as Categorias
        var catChas = new Categoria { NomeCategoria = "Chás e Infusões" };
        var catTemperos = new Categoria { NomeCategoria = "Temperos e Especiarias" };
        var catSuplementos = new Categoria { NomeCategoria = "Suplementos Naturais" };
        var catQueijos = new Categoria { NomeCategoria = "Queijos e Laticínios" };
        var catVinhos = new Categoria { NomeCategoria = "Vinhos e Bebidas" };
        var catGraos = new Categoria { NomeCategoria = "Grãos e Cereais" };

        // Adiciona as categorias ao contexto
        context.Categorias.AddRange(catChas, catTemperos, catSuplementos, catQueijos, catVinhos, catGraos);
        
        // Salva as categorias no banco em memória para gerar os IDs
        context.SaveChanges();

        // 2. Cria os Produtos, usando os IDs das categorias salvas
        context.Produtos.AddRange(
            // Chás
            new Produto 
            { 
                NomeProduto = "Chá de Camomila Orgânico", 
                Descricao = "Flores de camomila secas para infusão. Pacote 30g.", 
                Preco = 14.50m, 
                Estoque = 50, 
                IdCategoria = catChas.IdCategoria 
            },
            new Produto 
            { 
                NomeProduto = "Chá Verde Tostado (Hojicha)", 
                Descricao = "Chá verde japonês com baixo teor de cafeína. Pacote 50g.", 
                Preco = 22.90m, 
                Estoque = 30, 
                IdCategoria = catChas.IdCategoria 
            },
            // Temperos
            new Produto 
            { 
                NomeProduto = "Cúrcuma Pura (Açafrão-da-terra)", 
                Descricao = "Raiz de cúrcuma moída pura. Pacote 100g.", 
                Preco = 9.80m, 
                Estoque = 100, 
                IdCategoria = catTemperos.IdCategoria 
            },
            new Produto 
            { 
                NomeProduto = "Páprica Defumada Espanhola", 
                Descricao = "Páprica doce defumada (Pimentón). Lata 75g.", 
                Preco = 18.00m, 
                Estoque = 45, 
                IdCategoria = catTemperos.IdCategoria 
            },
            // Suplementos
            new Produto 
            { 
                NomeProduto = "Spirulina em Cápsulas", 
                Descricao = "Suplemento de microalga Spirulina. 60 cápsulas de 500mg.", 
                Preco = 49.90m, 
                Estoque = 25, 
                IdCategoria = catSuplementos.IdCategoria 
            },
             new Produto 
            { 
                NomeProduto = "Cloreto de Magnésio P.A.", 
                Descricao = "Cloreto de Magnésio P.A. em pó. Embalagem 100g.", 
                Preco = 15.00m, 
                Estoque = 60, 
                IdCategoria = catSuplementos.IdCategoria 
            },
            // Queijos
            new Produto 
            { 
                NomeProduto = "Queijo Minas Artesanal (Serro)", 
                Descricao = "Queijo curado artesanal da região do Serro-MG. Peça aprox. 500g.", 
                Preco = 38.00m, 
                Estoque = 20, 
                IdCategoria = catQueijos.IdCategoria 
            },
            // Vinhos
            new Produto 
            { 
                NomeProduto = "Vinho Tinto Orgânico (Syrah)", 
                Descricao = "Vinho tinto seco orgânico nacional. Garrafa 750ml.", 
                Preco = 72.00m, 
                Estoque = 15, 
                IdCategoria = catVinhos.IdCategoria 
            },
            // Grãos
             new Produto 
            { 
                NomeProduto = "Quinoa Real em Grãos", 
                Descricao = "Grãos de Quinoa Real orgânica. Pacote 250g.", 
                Preco = 19.90m, 
                Estoque = 40, 
                IdCategoria = catGraos.IdCategoria 
            }
        );

        // Salva os produtos no banco em memória
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();