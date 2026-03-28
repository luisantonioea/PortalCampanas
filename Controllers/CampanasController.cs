using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;
using System.Linq;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        public IActionResult Index()
        {
            var campanas = CampanaRepository.Campanas;
            return View(campanas);
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
    }
}