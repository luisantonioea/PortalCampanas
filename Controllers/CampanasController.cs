using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;
using System.Linq;
using System.Collections.Generic;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
        public IActionResult Index(string categoria, string estado)
        {
            var campanas = CampanaRepository.Campanas.OrderBy(c => c.Nombre).AsQueryable();

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
            ViewBag.MensajeLayout = "Layout Mejorado Activo";

            return View(campanas.ToList());
        }

        public IActionResult Detalle(int id)
        {
            var campana = CampanaRepository.Campanas.FirstOrDefault(c => c.Id == id);
            
            if (campana == null) 
            {
                return NotFound();
            }
            
            return View(campana);
        }

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