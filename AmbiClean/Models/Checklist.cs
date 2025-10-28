namespace AmbiClean.Models
{
    public class Checklist
    {
        public int Id { get; set; }              
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public bool Ativo { get; set; }          
        
        public Checklist(int id, string nome, string descricao, bool ativo)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }

        public Checklist() { }
    }
}
