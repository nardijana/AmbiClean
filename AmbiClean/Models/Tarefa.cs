namespace AmbiClean.Models
{
    public class Tarefa
    {
        public int Id { get; set; }             
        public int AreaId { get; set; }            
        public int ChecklistId { get; set; }      
        public string Nome { get; set; }         
        public string Descricao { get; set; }      
        public int MinutosEstimados { get; set; } 
        public bool Ativo { get; set; }

        public Tarefa(int id, int areaId, int checklistId, string nome, string descricao, int minutosEstimados, bool ativo)
        {
            Id = id;
            AreaId = areaId;
            ChecklistId = checklistId;
            Nome = nome;
            Descricao = descricao;
            MinutosEstimados = minutosEstimados;
            Ativo = ativo;
        }
    }
}
