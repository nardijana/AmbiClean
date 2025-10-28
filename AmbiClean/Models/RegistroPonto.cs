namespace AmbiClean.Models
{
    public class RegistroPonto
    {
        public int Id { get; set; }               
        public string TipoRegistro { get; set; }   
        public int ReferenciaId { get; set; }    
        public DateTime RegistradoEm { get; set; }  
        public string Dispositivo { get; set; }

        public RegistroPonto(int id, string tipoRegistro, int referenciaId, DateTime registradoEm, string dispositivo)
        {
            Id = id;
            TipoRegistro = tipoRegistro;
            ReferenciaId = referenciaId;
            RegistradoEm = registradoEm;
            Dispositivo = dispositivo;
        }

        public RegistroPonto() { }
    }
}
