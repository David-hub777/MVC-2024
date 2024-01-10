namespace MVC_2024.Models
{
    public class SerieModelo
    {
        public int Id { get; set; }
        public string NomSerie { get; set; }
        public MarcaModel Marca { get; set; }
        public int MarcaId { get; set; }
        
    }
}
