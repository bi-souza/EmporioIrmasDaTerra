using System.Text.Json; // usado para converter listas/objetos em JSON (salvar no Session)
using EmporioIrmasDaTerra.Models;
using EmporioIrmasDaTerra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmporioIrmasDaTerra.Controllers
{
    public class CarrinhoController : Controller
    {
        // chave usada para identificar o carrinho dentro da sessão
        private const string KEY = "CART";

        // repositório de produtos para buscar detalhes do produto (preço, nome etc.)
        private readonly IProdutoRepository _repo;

        // injeção de dependência do repositório
        public CarrinhoController(IProdutoRepository repo)
        {
            _repo = repo;
        }

        // 🔹 MÉTODOS AUXILIARES 🔹
        // Lê o carrinho atual da sessão
        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString(KEY); // lê o carrinho salvo (em JSON)
            return string.IsNullOrEmpty(json)
                ? new List<CartItem>() // se não houver nada, cria lista vazia
                : JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        // Salva a lista atualizada do carrinho na sessão
        private void SaveCart(List<CartItem> items)
            => HttpContext.Session.SetString(KEY, JsonSerializer.Serialize(items));

        // 🔹 EXIBIR O CARRINHO 🔹
        // GET /Carrinho
        public IActionResult Index()
        {
            var itens = GetCart(); // busca os itens do carrinho
            ViewBag.Total = itens.Sum(i => i.PrecoUnitario * i.Quantidade); // calcula total
            return View(itens); // envia os itens para a View Index.cshtml
        }

        // 🔹 ADICIONAR ITEM 🔹
        // POST /Carrinho/Adicionar/5?qtd=1
        [HttpPost]
        public async Task<IActionResult> Adicionar(int id, int qtd = 1)
        {
            // busca o produto pelo ID no repositório
            var produto = await _repo.GetById(id);
            if (produto == null) return NotFound(); // se não achar o produto, retorna erro 404

            var cart = GetCart(); // pega carrinho atual
            var item = cart.FirstOrDefault(i => i.IdProduto == produto.IdProduto);

            if (item == null)
            {
                // se o produto ainda não está no carrinho, adiciona como novo
                cart.Add(new CartItem
                {
                    IdProduto = produto.IdProduto,
                    Nome = produto.NomeProduto,
                    PrecoUnitario = produto.Preco,
                    Quantidade = Math.Max(1, qtd) // evita quantidade zero
                });
            }
            else
            {
                // se já existe, apenas soma a quantidade
                item.Quantidade += Math.Max(1, qtd);
            }

            SaveCart(cart); // salva o carrinho atualizado
            var referer = Request.Headers["Referer"].ToString(); //reques(objeto que representa a requisição do HTTP ATUAL  com .Headers é dos cabelcahos HTTP -- var referer(armazena o valor da URL anterior na vaiavel)
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer); // volta para a página de onde o usuário veio
            else
                return RedirectToAction("Index", "Home"); // fallback se não tiver referer

        }

        // 🔹 ATUALIZAR QUANTIDADE 🔹
        // POST /Carrinho/Atualizar
        [HttpPost]
        public IActionResult Atualizar(int id, int qtd)
        {
            var cart = GetCart();
            var it = cart.FirstOrDefault(i => i.IdProduto == id);
            if (it != null) it.Quantidade = Math.Max(1, qtd); // atualiza a quantidade
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // 🔹 REMOVER ITEM 🔹
        // POST /Carrinho/Remover/5
        [HttpPost]
        public IActionResult Remover(int id)
        {
            // cria uma nova lista sem o item removido
            var cart = GetCart().Where(i => i.IdProduto != id).ToList();
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // 🔹 FINALIZAR COMPRA 🔹
        // POST /Carrinho/Finalizar
        [HttpPost]
        public IActionResult Finalizar()
        {
            // calcula o total e limpa o carrinho
            var total = GetCart().Sum(i => i.PrecoUnitario * i.Quantidade);
            SaveCart(new List<CartItem>());
           HttpContext.Session.Remove("KEY");//  limpar o carrinho

            // mensagem temporária (aparece uma vez)
            TempData["Msg"] = $"Compra finalizada! Total: {total:C}.";
            return View("PedidoRealizado"); // agora quando clicar em finalizar compra vai aparecer essa mensagem da view
        }
        [HttpPost]
        public IActionResult FinalizarCompra()
        {
             HttpContext.Session.Remove("KEY");//  limpar o carrinho



            // Aqui futuramente você pode salvar o pedido no banco

            // Retorna uma página de confirmação
        return View("PedidoRealizado");
        }

    }
}
