using System.Collections.Generic;

namespace PortalCampanas.Models
{
    public class ResumenViewModel
    {
        public int TotalCampanas { get; set; }
        public int CampanasVigentes { get; set; }
        public int CampanasProximas { get; set; }
        public double PromedioDescuento { get; set; }
        public Dictionary<string, int> CantidadPorCanal { get; set; } = new Dictionary<string, int>();
    }
}