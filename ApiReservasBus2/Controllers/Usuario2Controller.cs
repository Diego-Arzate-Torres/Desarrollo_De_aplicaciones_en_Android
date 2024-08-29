using ApiReservasBus2.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiReservasBus2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Usuario2Controller : Controller
    {
        private AppDbContext2 _appDbContext;

        public Usuario2Controller(AppDbContext2 appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CrearUsuario(Usuario usuario)
        {
            _appDbContext.Usuario.Add(usuario);
            await _appDbContext.SaveChangesAsync();
            return Ok(usuario);
        }

        // Endpoint para listar todos los usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ListarTodos()
        {
            return await _appDbContext.Usuario.ToListAsync();
        }

        // Endpoint para buscar un usuario por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorId(int id)
        {
            var usuario = await _appDbContext.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // Endpoint para buscar un usuario por correo
        [HttpGet("buscarPorCorreo/{correo}")]
        public async Task<ActionResult<Usuario>> BuscarUsuarioPorCorreo(string correo)
        {
            var usuario = await _appDbContext.Usuario.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // Endpoint para eliminar un usuario por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _appDbContext.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            _appDbContext.Usuario.Remove(usuario);
            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }

        // Endpoint para actualizar un usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_appDbContext.Usuario.Any(e => e.Id == id))
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



        // Endpoint para buscar usuario por correo y contraseña
        [HttpGet("buscar")]
        public async Task<ActionResult<Usuario>> BuscarUsuario(string correo, string pwd)
        {
            var usuario = await _appDbContext.Usuario
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Pwd == pwd);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado o credenciales incorrectas.");
            }

            return Ok(usuario);
        }


    }
}
