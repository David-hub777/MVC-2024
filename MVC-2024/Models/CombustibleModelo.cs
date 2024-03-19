using Microsoft.EntityFrameworkCore;

namespace MVC_2024.Models
{
    public class CombustibleModelo
    {
        public int Id { get; set; }
        public string NomCombustible { get; set; }

        
        public List<VehiculoModelo> LosVehiculos { get; set; }
    }
    
}
