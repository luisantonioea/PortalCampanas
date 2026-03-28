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
    }
}