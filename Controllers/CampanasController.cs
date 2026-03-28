using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;
using System.Linq;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        public IActionResult Index(string categoria, string estado)
{
    // 1. Obtenemos todas las campañas de nuestra lista estática
    var campanas = CampanaRepository.Campanas.AsQueryable();

    // 2. Filtramos por Categoría si el usuario seleccionó una
    if (!string.IsNullOrEmpty(categoria))
    {
        campanas = campanas.Where(c => c.Categoria == categoria);
    }

    // 3. Filtramos por Estado si el usuario seleccionó uno (y que no sea "Todos")
    if (!string.IsNullOrEmpty(estado) && estado != "Todos")
    {
        campanas = campanas.Where(c => c.Estado == estado);
    }

    // 4. Guardamos la selección en el ViewBag para que los menús desplegables 
    // no se borren después de hacer clic en "Filtrar"
    ViewBag.CategoriaSeleccionada = categoria;
    ViewBag.EstadoSeleccionado = estado;

    // 5. Devolvemos la lista ya filtrada a la vista
    return View(campanas.ToList());
}

        // NUEVA ACCIÓN: Busca la campaña por su ID en la lista en memoria
        public IActionResult Detalle(int id)
        {
            var campana = CampanaRepository.Campanas.FirstOrDefault(c => c.Id == id);
            
            if (campana == null)
            {
                return NotFound(); // Retorna error 404 si el ID no existe
            }

            return View(campana);
        }
        public IActionResult Resumen()
{
    // Usamos ?? para evitar errores si la lista está vacía
    var campanas = CampanaRepository.Campanas ?? new List<PortalCampanas.Models.Campana>();

    var resumen = new PortalCampanas.Models.ResumenViewModel
    {
        TotalCampanas = campanas.Count,
        CampanasVigentes = campanas.Count(c => c.Estado == "Vigente"),
        CampanasProximas = campanas.Count(c => c.Estado == "Próxima"),
        PromedioDescuento = campanas.Any() ? System.Math.Round(campanas.Average(c => c.DescuentoPct), 2) : 0,
        CantidadPorCanal = campanas.Any() 
            ? campanas.GroupBy(c => c.Canal).ToDictionary(g => g.Key, g => g.Count()) 
            : new Dictionary<string, int>()
    };

    return View(resumen);
}
    }
}