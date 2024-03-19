using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_2024.Models
{
    public class VehiculoModelo
    {
        public int Id { get; set; }
        public string Matricula { get; set; }
        public string Color { get; set; }
        public SerieModelo Serie { get; set; }
        public int SerieId { get; set; }
        public CombustibleModelo Combustible { get; set; }
        public int CombustibleId { get; set; }


        [NotMapped]
        public List<int> ExtrasSeleccionados { get; set; }
        public List<VehiculoExtraModelo> VehiculoExtras { get; set; }

    }
}
