using EmporioIrmasDaTerra.Models;
using Microsoft.EntityFrameworkCore;

namespace EmporioIrmasDaTerra.Data
{
    // Esta é uma classe "estática", o que significa que não precisamos criar uma
    // instância dela para usar seus métodos.
    public static class SeedData
    {
        // Este é o método que o Program.cs irá chamar.
        // Ele recebe o "mapa" do banco (AppDbContext) para poder adicionar os dados.
        public static void Initialize(AppDbContext context)
        {
            // O context.Database.EnsureCreated() já deve ter sido chamado no Program.cs,
            // antes de chamar este método.

            // Verifica se o banco já tem categorias. Se tiver, pula o "seeding".
            if (context.Categorias.Any())
            {
                return; // O banco de dados já foi populado
            }           

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
                    NomeProduto = "Chá de Camomila",
                    Descricao = "Flores de camomila secas para infusão. Pacote 30g.",
                    Preco = 14.50m,
                    Peso = 0.030m,
                    Estoque = 50,
                    IdCategoria = catChas.IdCategoria,
                    ImagemUrl = "/images/produtos/cha-de-camomila.jpg"
                },
                new Produto
                {
                    NomeProduto = "Chá Verde Tostado (Hojicha)",
                    Descricao = "Chá verde japonês com baixo teor de cafeína. Pacote 50g.",
                    Preco = 22.90m,
                    Peso = 0.050m,
                    Estoque = 30,
                    IdCategoria = catChas.IdCategoria,
                    ImagemUrl = "/images/produtos/cha-verde-tostado.jpg"
                },
                // Temperos
                new Produto
                {
                    NomeProduto = "Cúrcuma Pura (Açafrão-da-terra)",
                    Descricao = "Raiz de cúrcuma moída pura. Pacote 100g.",
                    Preco = 9.80m,
                    Peso = 0.10m,
                    Estoque = 100,
                    IdCategoria = catTemperos.IdCategoria,
                    ImagemUrl = "/images/produtos/curcuma.jpg"
                },
                new Produto
                {
                    NomeProduto = "Páprica Defumada",
                    Descricao = "Páprica doce defumada (Pimentón). Pacote 75g.",
                    Preco = 18.00m,
                    Peso = 0.075m,
                    Estoque = 45,
                    IdCategoria = catTemperos.IdCategoria,
                    EmDestaque = true,
                    ImagemUrl = "/images/produtos/paprica-defumada.jpg"
                },
                // Suplementos
                new Produto
                {
                    NomeProduto = "Spirulina em Cápsulas",
                    Descricao = "Suplemento de microalga Spirulina. 60 cápsulas de 500mg.",
                    Preco = 49.90m,
                    Peso = 0.030m,
                    Estoque = 25,
                    IdCategoria = catSuplementos.IdCategoria,
                    ImagemUrl = "/images/produtos/spirulina-capsulas.jpg"
                },
                new Produto
                {
                    NomeProduto = "Cloreto de Magnésio",
                    Descricao = "Cloreto de Magnésio P.A. em pó. Embalagem 100g.",
                    Preco = 15.00m,
                    Peso = 0.100m,
                    Estoque = 60,
                    IdCategoria = catSuplementos.IdCategoria,
                    ImagemUrl = "/images/produtos/cloreto-magnesio.jpg"
                },
                // Queijos
                new Produto
                {
                    NomeProduto = "Queijo Minas Artesanal",
                    Descricao = "Queijo curado artesanal da região do Serro-MG. Peça aprox. 500g.",
                    Preco = 38.00m,
                    Peso = 0.500m,
                    Estoque = 20,
                    IdCategoria = catQueijos.IdCategoria,
                    EmDestaque = true,
                    ImagemUrl ="/images/produtos/queijo-minas.jpg"

                },
                // Vinhos
                new Produto
                {
                    NomeProduto = "Vinho Tinto",
                    Descricao = "Vinho tinto seco orgânico nacional. Garrafa 750ml.",
                    Preco = 72.00m,
                    Peso = 0.750m,
                    Estoque = 15,
                    IdCategoria = catVinhos.IdCategoria,
                    ImagemUrl = "/images/produtos/vinho-tinto.jpg"
                },
                // Grãos
                new Produto
                {
                    NomeProduto = "Quinoa Real em Grãos",
                    Descricao = "Grãos de Quinoa Real orgânica. Pacote 250g.",
                    Preco = 19.90m,
                    Peso = 0.250m,
                    Estoque = 40,
                    IdCategoria = catGraos.IdCategoria,
                    EmDestaque = true,
                    ImagemUrl = "/images/produtos/quinoa-graos.jpg"
                }
            );

            // Salva os produtos no banco em memória
            context.SaveChanges();
            
        }
    }
}