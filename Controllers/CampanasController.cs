using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        public IActionResult Index()
        {
            var campanas = CampanaRepository.Campanas;
            return View(campanas);
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