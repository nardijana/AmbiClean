namespace AmbiClean.Models
{
    public class ExecucaoTarefa
    {
        public int Id { get; set; }            
        public int TarefaId { get; set; }         
        public int UsuarioId { get; set; }      
        public DateTime AgendadoInicio { get; set; } 
        public DateTime AgendadoFim { get; set; }    
        public DateTime Inicio { get; set; }        
        public DateTime Fim { get; set; }           
        public string Status { get; set; }           
        public string Observacoes { get; set; }     
        public DateTime CriadoEm { get; set; }

        public ExecucaoTarefa(int id, int tarefaId, int usuarioId, DateTime agendadoInicio, DateTime agendadoFim, DateTime inicio, DateTime fim, string status, string observacoes, DateTime criadoEm)
        {
            Id = id;
            TarefaId = tarefaId;
            UsuarioId = usuarioId;
            AgendadoInicio = agendadoInicio;
            AgendadoFim = agendadoFim;
            Inicio = inicio;
            Fim = fim;
            Status = status;
            Observacoes = observacoes;
            CriadoEm = criadoEm;
        }

        public ExecucaoTarefa() { }
    }
}
