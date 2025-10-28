namespace AmbiClean.Models
{
    public class AtribuicaoUsuarioArea
    {
        public int Id { get; set; }              
        public int UsuarioId { get; set; }        
        public int AreaId { get; set; }      
        public DateTime AtribuidoEm { get; set; }

        public AtribuicaoUsuarioArea(int id, int usuarioId, int areaId, DateTime atribuidoEm)
        {
            Id = id;
            UsuarioId = usuarioId;
            AreaId = areaId;
            AtribuidoEm = atribuidoEm;
        }
    }
}
