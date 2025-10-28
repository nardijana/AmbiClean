namespace AmbiClean.Models
{
    class ResultadoChecklist
    {
        public int Id { get; set; }                
        public int ExecucaoId { get; set; }       
        public int ChecklistId { get; set; }      
        public bool Marcado { get; set; }          
        public string Comentario { get; set; }      
        public DateTime MarcadoEm { get; set; }

        public ResultadoChecklist(int id, int execucaoId, int checklistId, bool marcado, string comentario, DateTime marcadoEm)
        {
            Id = id;
            ExecucaoId = execucaoId;
            ChecklistId = checklistId;
            Marcado = marcado;
            Comentario = comentario;
            MarcadoEm = marcadoEm;
        }
    }
}
