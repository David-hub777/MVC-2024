using Microsoft.EntityFrameworkCore;

namespace MVC_2024.Models
{
    public class Contexto : DbContext
    {
        public DbSet<marcaModel> Marcas { get; set; }//Debe coincidir con la TABLA de la DB
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {//Crea la DB Automatic
        }
    }
}
