using Microsoft.EntityFrameworkCore;

namespace MVC_2024.Models
{
    public class MarcaModel
    {
        
        public int Id { get; set; }
        public string NomMarca { get; set; } //= string.Empty;
        public List<SerieModelo> LasSeries { get; set; }
        //public MarcaModel(MarcaModel marca2)
        //{
        //    this.NomMarca = marca2.NomMarca;
        //    this.Id = marca2.Id;
            //this.LasSeries = new List<SerieModelo>();
            //if (marca2.LasSeries != null)
            //{
            //    this.LasSeries.AddRange(marca2.LasSeries);
            //}
        //}
    }
}
