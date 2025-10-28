namespace AmbiClean.Models
{
    public class ItemChecklist
    {
        public int Id { get; set; }               
        public int ChecklistId { get; set; }       
        public string Descricao { get; set; } = string.Empty;
        public int Posicao { get; set; }

        public ItemChecklist(int id, int checklistId, string descricao, int posicao)
        {
            Id = id;
            ChecklistId = checklistId;
            Descricao = descricao;
            Posicao = posicao;
        }
    }
}
