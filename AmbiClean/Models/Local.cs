using System.Globalization;

namespace AmbiClean.Models
{
    public class Local
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string  Endereco { get; set; }
        public bool Ativo { get; set; }

        public Local(int id, string nome, string endereco, bool ativo)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Ativo = ativo;
        }

        public Local() { }
    }
}
