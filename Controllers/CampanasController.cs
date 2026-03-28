using Microsoft.AspNetCore.Mvc;
using PortalCampanas.Data;
using System.Linq;

namespace PortalCampanas.Controllers
{
    public class CampanasController : Controller
    {
feature/mejora-layout
        public IActionResult Index()
{
    // Modificamos esta misma zona para que choque directamente con los Filtros
    var campanasOrdenadas = CampanaRepository.Campanas.OrderBy(c => c.Nombre).ToList();
    
    // Un mensaje de prueba para el layout
    ViewBag.MensajeLayout = "Layout Mejorado Activo";
    
    return View(campanasOrdenadas);
}

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
main
    }
}