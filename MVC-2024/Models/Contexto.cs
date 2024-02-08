using Microsoft.EntityFrameworkCore;
using static MVC_2024.Controllers.VehiculoController;

namespace MVC_2024.Models
{
    public class Contexto : DbContext
    {
        public DbSet<MarcaModel> Marcas { get; set; }//Debe coincidir con la TABLA de la DB
        public DbSet<SerieModelo> Series { get; set; }//Debe coincidir con la TABLA de la DB
        public DbSet<VehiculoModelo> Vehiculos { get; set; }//Debe coincidir con la TABLA de la DB
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoTotal>().HasNoKey();
            // // Metodo de Agustin (Mas facil lo de arriba @Made_By_Copilot)
            //modelBuilder.Entity<VehiculoTotal>(
            //        eb =>
            //        {
            //            eb.HasNoKey();
            //        }
            //    );
        }
        public DbSet<VehiculoTotal> ViewTotal { get; set; }//Debe coincidir con la TABLA de la DB
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {//Crea la DB Automatic

        }
    }
}
