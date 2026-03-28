using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        public IActionResult Index()
{
    // Modificamos esta misma zona para que choque directamente con los Filtros
    var campanasOrdenadas = CampanaRepository.Campanas.OrderBy(c => c.Nombre).ToList();
    
    // Un mensaje de prueba para el layout
    ViewBag.MensajeLayout = "Layout Mejorado Activo";
    
    return View(campanasOrdenadas);
}
    }
}