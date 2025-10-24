// Models/Usuario.cs (Corrigido para evitar warnings CS8618)

namespace EmporioIrmasDaTerra.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        
        // Inicializa strings não-nulas para evitar CS8618
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty; 
        
        // Use '?' para campos opcionais (se for o caso)
        public string? Telefone { get; set; }
        public string? Cpf { get; set; } 
        
        // Dê um valor padrão, ou inicialize com string.Empty
        public string Papel { get; set; } = "Cliente"; 
    }
}