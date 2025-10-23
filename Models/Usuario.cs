namespace EmporioIrmasDaTerra.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        // Em um projeto real, a senha NUNCA deve ser armazenada como string, 
        // mas sim como hash. Para este exercício em memória, vamos simplificar.
        public string Senha { get; set; } 
        
        // Novos Campos para Cadastro de E-commerce
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        
        // Campo para a separação Admin/Cliente (Role)
        public string Papel { get; set; } // "Admin" ou "Cliente"
    }
}