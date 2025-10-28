namespace AmbiClean.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Cargo(int id, string nome, string descricao)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        } 

        public Cargo() { }
    }
}
