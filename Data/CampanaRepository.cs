using PortalCampanas.Models;

namespace PortalCampanas.Data
{
    public static class CampanaRepository
    {
        public static List<Campana> Campanas = new List<Campana>
        {
            new Campana { Id = 1, Nombre = "CyberWow Electro", Categoria = "Electro", Estado = "Vigente", FechaInicio = new DateTime(2026, 3, 15), FechaFin = new DateTime(2026, 3, 20), DescuentoPct = 30, Canal = "Web", Descripcion = "Descuentos en electrodomésticos." },
            new Campana { Id = 2, Nombre = "Renueva tu Hogar", Categoria = "Hogar", Estado = "Vigente", FechaInicio = new DateTime(2026, 3, 1), FechaFin = new DateTime(2026, 4, 1), DescuentoPct = 25, Canal = "Tienda", Descripcion = "Muebles y decoración." },
            new Campana { Id = 3, Nombre = "Tech Days", Categoria = "Tecnología", Estado = "Próxima", FechaInicio = new DateTime(2026, 5, 1), FechaFin = new DateTime(2026, 5, 5), DescuentoPct = 35, Canal = "Web", Descripcion = "Lo último en tecnología." }
            // Puedes agregar más para que coincidan con la imagen de ejemplo
        };
    }
}