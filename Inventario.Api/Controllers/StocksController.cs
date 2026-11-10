using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly InventarioDBContext _context;

        public StocksController(InventarioDBContext context)
        {
            _context = context;
        }

        // Consultar todos los stocks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            return await _context.Stocks.ToListAsync();
        }

        // Consultar un stock por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
                return NotFound();

            return stock;
        }

        // Registrar un stock
        [HttpPost]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            var bodega = await _context.Bodegas.FindAsync(stock.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock),
                new { id = stock.IdStock }, stock);
        }

        // Actualizar un stock
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStock(int id, Stock stock)
        {
            if (id != stock.IdStock)
                return BadRequest();

            var existe = await _context.Stocks.FindAsync(id);

            if (existe == null)
                return NotFound();

            var bodega = await _context.Bodegas.FindAsync(stock.IdBodega);

            if (bodega == null)
                return BadRequest("La bodega no existe.");

            existe.IdBodega = stock.IdBodega;
            existe.IdProducto = stock.IdProducto;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un stock
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
                return NotFound();

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}