namespace EmporioIrmasDaTerra.Models
{
    public class CartItem
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; } = "";
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }
    }
}
