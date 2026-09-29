using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;

namespace CACET.Controllers
{
    public class AuditoriaController : Controller
    {
        private readonly SistemaLegalContext _context;

        public AuditoriaController(SistemaLegalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string tabla, string accion, DateTime? fechaInicio, DateTime? fechaFin)
        {
            var query = _context.Auditorias.AsQueryable();

            if (!string.IsNullOrEmpty(tabla))
                query = query.Where(a => a.Tabla.Contains(tabla));
            
            if (!string.IsNullOrEmpty(accion))
                query = query.Where(a => a.Accion == accion);

            if (fechaInicio.HasValue)
                query = query.Where(a => a.FechaHora >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(a => a.FechaHora <= fechaFin.Value.AddDays(1).AddTicks(-1));

            var logs = await query.OrderByDescending(a => a.FechaHora).ToListAsync();

            ViewBag.Tabla = tabla;
            ViewBag.Accion = accion;
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View(logs);
        }
    }
}
