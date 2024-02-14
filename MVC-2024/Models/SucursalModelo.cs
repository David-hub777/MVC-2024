namespace MVC_2024.Models
{
    public class SucursalModelo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public VehiculoModelo Coche { get; set; }
        public int VejiculoId { get; set; }
    }
}
