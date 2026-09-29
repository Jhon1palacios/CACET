using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CACET.Controllers
{
    public class ReportesController : Controller
    {
        private readonly SistemaLegalContext _context;

        public ReportesController(SistemaLegalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Estadisticas()
        {
            var q = _context.CasosJudiciales.Where(c => c.Estado != "Eliminado");
            
            ViewBag.TotalCasos = await q.CountAsync();
            ViewBag.Activos = await q.CountAsync(c => c.Clasificacion == "Procesos activos");
            ViewBag.EnTramite = await q.CountAsync(c => c.Resolucion == "EN TRAMITE");
            ViewBag.Finalizados = await q.CountAsync(c => c.Clasificacion == "Procesos finalizados");
            ViewBag.Acuerdos = await q.CountAsync(c => c.Clasificacion == "Acuerdos de pago");
            ViewBag.Archivados = await q.CountAsync(c => c.Estado == "Archivado"); // o alguna logica
            
            var porTipo = await q.GroupBy(c => c.TipoProceso)
                                 .Select(g => new { Tipo = g.Key, Cantidad = g.Count() })
                                 .ToListAsync();
            
            ViewBag.TiposProcesoLabels = string.Join(",", porTipo.Select(x => $"\"{x.Tipo}\""));
            ViewBag.TiposProcesoData = string.Join(",", porTipo.Select(x => x.Cantidad));

            return View();
        }

        public async Task<IActionResult> ExportarExcel()
        {
            var casos = await _context.CasosJudiciales
                .Where(c => c.Estado != "Eliminado")
                .OrderByDescending(c => c.FechaRegistro)
                .ToListAsync();
                
            return View(casos);
        }
    }
}
