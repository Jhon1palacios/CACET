using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CACET.Data;
using System.Threading.Tasks;
namespace CACET.Controllers {
    public class ClientesController : Controller {
        private readonly SistemaLegalContext _context;
        public ClientesController(SistemaLegalContext context) { _context = context; }
        public async Task<IActionResult> Index() {
            var clientes = await _context.Socios.ToListAsync();
            return View(clientes);
        }
    }
}
