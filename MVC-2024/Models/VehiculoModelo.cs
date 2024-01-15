namespace MVC_2024.Models
{
    public class VehiculoModelo
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Color { get; set; }
        public SerieModelo Serie { get; set; }
        public int SerieId { get; set; }
    }
}
