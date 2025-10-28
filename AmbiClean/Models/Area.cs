using System.Globalization;

namespace AmbiClean.Models
{
    public class Area
    {
        public int Id { get; set; }
        public int LocalId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public Area(int id, int localId, string nome, string descricao)
        {
            Id = id;
            LocalId = localId;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
