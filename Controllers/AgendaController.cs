using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;
using System.Threading.Tasks;
using System.Linq;
namespace CACET.Controllers {
    public class AgendaController : Controller {
        private readonly SistemaLegalContext _context;
        public AgendaController(SistemaLegalContext context) { _context = context; }
        public async Task<IActionResult> Index() {
            var movimientos = await _context.Movimientos.OrderByDescending(m => m.FechaMovimiento).ToListAsync();
            return View(movimientos);
        }
    }
}
