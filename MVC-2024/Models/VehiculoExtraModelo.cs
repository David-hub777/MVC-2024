namespace MVC_2024.Models
{
    public class VehiculoExtraModelo
    {
        public int Id { get; set; }
        public int extraId { get; set; }
        public ExtraModelo extra { get; set; }
        public int vehiculoId { get; set; }
        public VehiculoModelo vehiculo { get; set; }
        
    }
}
