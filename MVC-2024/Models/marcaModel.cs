using Microsoft.EntityFrameworkCore;

namespace MVC_2024.Models
{
    public class marcaModel
    {
        
        public int Id { get; set; }
        public string NomMarca { get; set; } //= string.Empty;
        public marcaModel()
        {
        }
    }
}
