using ApiReservasBus2.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiReservasBus2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutobusController : Controller
    {
        private AppDbContext2 _appDbContext2;
        public AutobusController(AppDbContext2 appDbContext2)
        {
            _appDbContext2 = appDbContext2;
        }
        [HttpPost]
        public async Task<ActionResult<Autobus>> CrearAutobus(Autobus autobus)
        {
            _appDbContext2.Autobus.Add(autobus);
            await _appDbContext2.SaveChangesAsync();
            return Ok(autobus);
        }
        //Endpoint para enlistar a los autobuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autobus>>> ListarTodos()
        {
            return await _appDbContext2.Autobus.ToListAsync();
        }

        //Endpoint para buscar autobus por id
        [HttpGet("{id}")]
        public async Task<ActionResult<Autobus>> BuscarAutobusPorId(int id)
        {
            var autobus = await _appDbContext2.Autobus.FindAsync(id);
            if (autobus == null)
            {
                return NotFound();
            }
            return Ok(autobus);
        }
        //Endpoint para buscar autobus por placas
        [HttpGet("buscarPorPlacas/{placas}")]
        public async Task<ActionResult<Autobus>> BuscarAutobusPorPlacas(string placas)
        {
            var autobus = await _appDbContext2.Autobus.FirstOrDefaultAsync(u => u.placas == placas);
            if (autobus == null)
            {
                return NotFound();
            }
            return Ok(autobus);
        }
        // Endpoint para eliminar un autobus por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarAutobus(int id)
        {
            var autobus = await _appDbContext2.Autobus.FindAsync(id);
            if (autobus == null)
            {
                return NotFound();
            }
            _appDbContext2.Autobus.Remove(autobus);
            await _appDbContext2.SaveChangesAsync();
            return NoContent();
        }

        // Endpoint para actualizar un autobus
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, Autobus autobus)
        {
            if (id != autobus.id)
            {
                return BadRequest();
            }

            _appDbContext2.Entry(autobus).State = EntityState.Modified;

            try
            {
                await _appDbContext2.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDbContext2.Autobus.Any(e => e.id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Endpoint para buscar autobus por placas y capacidad
        [HttpGet("buscar")]
        public async Task<ActionResult<Autobus>> BuscarAutobus(string placas, int capacidad)
        {
            var autobus = await _appDbContext2.Autobus
                .FirstOrDefaultAsync(u => u.placas == placas && u.capacidad == capacidad);

            if (autobus == null)
            {
                return NotFound("Autobus no encontrado o credenciales incorrectas.");
            }

            return Ok(autobus);
        }
    }
}
