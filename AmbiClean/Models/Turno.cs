namespace AmbiClean.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public string Descricao { get; set; }

        public Turno(int id, string nome, TimeSpan horaInicio, TimeSpan horaFim, string descricao)
        {
            Id = id;
            Nome = nome;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Descricao = descricao;
        }
    }
}
