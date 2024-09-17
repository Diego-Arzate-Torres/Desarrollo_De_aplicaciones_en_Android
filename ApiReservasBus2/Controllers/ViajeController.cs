using ApiReservasBus2.Modelos;
using Microsoft.AspNetCore.Mvc;
using ApiReservasBus2.Modelos.AppDbContext2;
using Microsoft.EntityFrameworkCore;

namespace ApiReservasBus2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViajeController : Controller
    {        
        private AppDbContext2 _appDbContext2;
        public ViajeController(AppDbContext2 appDbContext2)
        {
            _appDbContext2 = appDbContext2;
        }
        [HttpPost]
        
        public async Task<ActionResult<Viaje>> CrearViaje(Viaje destino)
        {
            _appDbContext2.Autobus.Add(destino);
            await _appDbContext2.SaveChangesAsync();
            return Ok(destino);
        }
        //Endpoint para enlistar a los viajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viaje>>> ListarTodos()
        {
            return await _appDbContext2.Viaje.ToListAsync();
        }
        //Endpoint para buscar viajes por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Autobus>> BuscarDestinoPorId(int id)
        {
            var destino = await _appDbContext2.Autobus.FindAsync(id);
            if (destino == null)
            {
                return NotFound();
            }
            return Ok(destino);
        }
        // Endpoint para buscar viaje por destino 
        [HttpGet("buscar")]
        public async Task<ActionResult<Viaje>> BuscarAutobus(string placas)
        {
            var autobus = await _appDbContext2.Autobus
                .FirstOrDefaultAsync(u => u.placas == placas);

            if (autobus == null)
            {
                return NotFound("Autobus no encontrado o credenciales incorrectas.");
            }

            return Ok(autobus);
        }
    }
}
