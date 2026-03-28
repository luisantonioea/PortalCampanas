using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;
using System.Linq;
using System.Collections.Generic; // Necesario para el Resumen

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        // 1. INDEX (Fusión: Tiene los Filtros, el Ordenamiento y el Layout)
        public IActionResult Index(string categoria, string estado)
        {
            // Ordenamos alfabéticamente (de la rama layout)
            var campanas = CampanaRepository.Campanas.OrderBy(c => c.Nombre).AsQueryable();

            // Aplicamos los filtros (de la rama filtros)
            if (!string.IsNullOrEmpty(categoria))
            {
                campanas = campanas.Where(c => c.Categoria == categoria);
            }

            if (!string.IsNullOrEmpty(estado) && estado != "Todos")
            {
                campanas = campanas.Where(c => c.Estado == estado);
            }

            ViewBag.CategoriaSeleccionada = categoria;
            ViewBag.EstadoSeleccionado = estado;
            
            // Mensaje del layout (de la rama layout)
            ViewBag.MensajeLayout = "Layout Mejorado Activo";

            return View(campanas.ToList());
        }

        // 2. DETALLE DE CAMPAÑA
        public IActionResult Detalle(int id)
        {
            var campana = CampanaRepository.Campanas.FirstOrDefault(c => c.Id == id);
            
            if (campana == null)
            {
                return NotFound();
            }

            return View(campana);
        }

        // 3. RESUMEN DE INDICADORES
        public IActionResult Resumen()
        {
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