namespace PortalCampanas.Models
{
    public class Campana
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; } // Electro, Hogar, Moda, Tecnología
        public string Estado { get; set; } // Vigente, Próxima, Finalizada
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double DescuentoPct { get; set; }
        public string Canal { get; set; } // Web, App, Tienda
        public string Descripcion { get; set; }
    }
}