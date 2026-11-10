using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegasController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public BodegasController(InventarioDBContext context)
        {
            _context = context;
        }

        // Consultar todas las bodegas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
        {
            return await _context.Bodegas.ToListAsync();
        }

        // Consultar una bodega por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
                return NotFound();

            return bodega;
        }

        // Registrar una bodega
        [HttpPost]
        public async Task<ActionResult<Bodega>> PostBodega(Bodega bodega)
        {
            _context.Bodegas.Add(bodega);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBodega),
                new { id = bodega.IdBodega }, bodega);
        }

        // Actualizar una bodega
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBodega(int id, Bodega bodega)
        {
            if (id != bodega.IdBodega)
                return BadRequest();

            var existe = await _context.Bodegas.FindAsync(id);

            if (existe == null)
                return NotFound();

            // Actualmente Bodega solo tiene IdBodega.
            // No tenemos otros campos para actualizar.
            return NoContent();
        }

        // Eliminar una bodega
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
                return NotFound();

            _context.Bodegas.Remove(bodega);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}