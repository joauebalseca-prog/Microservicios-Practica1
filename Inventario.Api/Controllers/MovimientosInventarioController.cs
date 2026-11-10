using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosInventarioController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public MovimientosInventarioController(InventarioDBContext context)
        {
            _context = context;
        }

        // Consultar todos los movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        {
            return await _context.MovimientosInventario.ToListAsync();
        }

        // Consultar un movimiento por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>> GetMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
                return NotFound();

            return movimiento;
        }

        // Registrar un movimiento
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> PostMovimiento(MovimientoInventario movimiento)
        {
            var bodega = await _context.Bodegas.FindAsync(movimiento.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimiento),
                new { id = movimiento.IdMovimientoInv }, movimiento);
        }

        // Actualizar un movimiento
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento(int id, MovimientoInventario movimiento)
        {
            if (id != movimiento.IdMovimientoInv)
                return BadRequest();

            var existe = await _context.MovimientosInventario.FindAsync(id);

            if (existe == null)
                return NotFound();

            var bodega = await _context.Bodegas.FindAsync(movimiento.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            existe.IdBodega = movimiento.IdBodega;
            existe.IdProducto = movimiento.IdProducto;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un movimiento
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
                return NotFound();

            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}