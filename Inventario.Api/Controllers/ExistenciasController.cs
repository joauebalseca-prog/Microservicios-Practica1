using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciasController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public ExistenciasController(InventarioDBContext context)
        {
            _context = context;
        }

        // Consultar todas las existencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencias>>> GetExistencias()
        {
            return await _context.Existencias.ToListAsync();
        }

        // Consultar existencias por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencias>> GetExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
                return NotFound();

            return existencia;
        }

        // Registrar existencias
        [HttpPost]
        public async Task<ActionResult<Existencias>> PostExistencia(Existencias existencia)
        {
            var bodega = await _context.Bodegas.FindAsync(existencia.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            _context.Existencias.Add(existencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExistencia),
                new { id = existencia.IdExistencias }, existencia);
        }

        // Actualizar existencias
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExistencia(int id, Existencias existencia)
        {
            if (id != existencia.IdExistencias)
                return BadRequest();

            var existe = await _context.Existencias.FindAsync(id);

            if (existe == null)
                return NotFound();

            var bodega = await _context.Bodegas.FindAsync(existencia.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            existe.IdBodega = existencia.IdBodega;
            existe.IdProducto = existencia.IdProducto;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar existencias
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
                return NotFound();

            _context.Existencias.Remove(existencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}