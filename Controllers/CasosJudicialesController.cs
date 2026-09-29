using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;
using CACET.Models;

namespace CACET.Controllers
{
    public class CasosJudicialesController : Controller
    {
        private readonly SistemaLegalContext _context;

        public CasosJudicialesController(SistemaLegalContext context)
        {
            _context = context;
        }



        // GET: /CasosJudiciales
        public async Task<IActionResult> Index(string? busqueda, string? abogado, string? tipoProceso, string? clasificacion)
        {
            var query = _context.CasosJudiciales.Where(c => c.Estado != "Eliminado").AsQueryable();
            if (!string.IsNullOrEmpty(busqueda))
                query = query.Where(c => c.ApellidosNombres.Contains(busqueda) || c.NumeroProceso.Contains(busqueda) || c.NumeroPagare.Contains(busqueda));
            if (!string.IsNullOrEmpty(abogado))
                query = query.Where(c => c.AbogadoAsignado == abogado);
            if (!string.IsNullOrEmpty(tipoProceso))
                query = query.Where(c => c.TipoProceso == tipoProceso);
            if (!string.IsNullOrEmpty(clasificacion))
                query = query.Where(c => c.Clasificacion == clasificacion);
                
            var casos = await query.OrderByDescending(c => c.FechaRegistro).ToListAsync();
            ViewBag.Abogados = await _context.CasosJudiciales.Select(c => c.AbogadoAsignado).Distinct().OrderBy(a => a).ToListAsync();

            ViewBag.UsandoDemo = false;
            ViewBag.Busqueda = busqueda;
            ViewBag.AbogadoFiltro = abogado;
            ViewBag.TipoProcesoFiltro = tipoProceso;
            ViewBag.ClasificacionFiltro = clasificacion;
            return View(casos);
        }

        // GET: KPIs JSON
        [HttpGet]
        public async Task<IActionResult> GetEstadisticas()
        {
            var q = _context.CasosJudiciales.Where(c => c.Estado != "Eliminado");
            var total      = await q.CountAsync();
            var activos    = await q.CountAsync(c => c.Clasificacion == "Procesos activos");
            var enTramite  = await q.CountAsync(c => c.Resolucion == "EN TRAMITE");
            var finalizados= await q.CountAsync(c => c.Clasificacion == "Procesos finalizados");
            var acuerdos   = await q.CountAsync(c => c.Clasificacion == "Acuerdos de pago");
            var montoTotal = await q.SumAsync(c => (decimal?)c.MontoPagare) ?? 0;
            return Json(new { total, activos, enTramite, finalizados, acuerdos, montoTotal, demo = false });
        }

        // GET: Detalle
        public async Task<IActionResult> Detalle(int id)
        {
            var caso = await _context.CasosJudiciales.FindAsync(id);
            if (caso == null) return NotFound();
            return PartialView("_DetalleCaso", caso);
        }

        // GET: Crear
        public IActionResult Crear() => PartialView("_FormCaso", new CasoJudicial());

        // POST: Crear
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CasoJudicial caso)
        {
            if (ModelState.IsValid)
            {
                caso.FechaRegistro = DateTime.Now;
                caso.Estado = "Activo";
                _context.Add(caso);
                await _context.SaveChangesAsync();
                TempData["Exito"] = "Proceso judicial creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_FormCaso", caso);
        }

        // GET: Editar
        public async Task<IActionResult> Editar(int id)
        {
            var caso = await _context.CasosJudiciales.FindAsync(id);
            if (caso == null) return NotFound();
            return PartialView("_FormCaso", caso);
        }

        // POST: Editar
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CasoJudicial caso)
        {
            if (id != caso.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(caso); 
                await _context.SaveChangesAsync(); 
                TempData["Exito"] = "Proceso actualizado."; 
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_FormCaso", caso);
        }

        // POST: Eliminar
        [HttpPost, ActionName("Eliminar")][ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            var caso = await _context.CasosJudiciales.FindAsync(id);
            if (caso != null) 
            { 
                caso.Estado = "Eliminado"; 
                _context.Update(caso); 
                await _context.SaveChangesAsync(); 
                TempData["Exito"] = "Proceso eliminado correctamente (oculto)."; 
            }
            return RedirectToAction(nameof(Index));
        }
    }
}