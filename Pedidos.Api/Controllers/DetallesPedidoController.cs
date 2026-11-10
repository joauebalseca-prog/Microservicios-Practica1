using Pedidos.Api.Data;
using Pedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallesPedidoController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public DetallesPedidoController(PedidosDBContext context)
        {
            _context = context;
        }

        // Consultar todos los detalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetalles()
        {
            return await _context.DetallesPedido.ToListAsync();
        }

        // Consultar un detalle por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>> GetDetalle(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
                return NotFound();

            return detalle;
        }

        // Registrar un detalle
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetalle(DetallePedido detalle)
        {
            var pedido = await _context.Pedidos.FindAsync(detalle.IdPedido);

            if (pedido == null)
                return BadRequest("El pedido no existe.");

            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalle),
                new { id = detalle.IdDetallePedido }, detalle);
        }

        // Actualizar un detalle
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalle(int id, DetallePedido detalle)
        {
            if (id != detalle.IdDetallePedido)
                return BadRequest();

            var existe = await _context.DetallesPedido.FindAsync(id);

            if (existe == null)
                return NotFound();

            var pedido = await _context.Pedidos.FindAsync(detalle.IdPedido);

            if (pedido == null)
                return BadRequest("El pedido no existe.");

            existe.IdPedido = detalle.IdPedido;
            existe.IdProducto = detalle.IdProducto;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un detalle
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalle(int id)
        {
            var detalle = await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
                return NotFound();

            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}