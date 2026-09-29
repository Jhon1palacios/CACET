using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;
using System.Threading.Tasks;
namespace CACET.Controllers {
    public class AbogadosController : Controller {
        private readonly SistemaLegalContext _context;
        public AbogadosController(SistemaLegalContext context) { _context = context; }
        public async Task<IActionResult> Index() {
            var abogados = await _context.Abogados.ToListAsync();
            return View(abogados);
        }
    }
}
