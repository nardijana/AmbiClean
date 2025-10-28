namespace AmbiClean.Models
{
    public class Usuario
    {
        public int Id { get; set; }                    
        public string NomeCompleto { get; set; }       
        public string Email { get; set; }              
        public string Senha { get; set; }             
        public int TurnoId { get; set; }               
        public string Telefone { get; set; }           
        public bool Ativo { get; set; }               
        public int CargoId { get; set; }
    }
}
