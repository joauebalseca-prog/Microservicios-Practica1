using Pedidos.Api.Data;
using Pedidos.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosDBContext _context;

        public PedidosController(PedidosDBContext context)
        {
            _context = context;
        }

        // Consultar todos los pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos.ToListAsync();
        }

        // Consultar un pedido por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound();

            return pedido;
        }

        // Registrar un pedido
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            var cliente = await _context.Clientes.FindAsync(pedido.IdCliente);

            if (cliente == null)
                return BadRequest("El cliente no existe.");

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido),
                new { id = pedido.IdPedido }, pedido);
        }

        // Actualizar un pedido
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
                return BadRequest();

            var existe = await _context.Pedidos.FindAsync(id);

            if (existe == null)
                return NotFound();

            var cliente = await _context.Clientes.FindAsync(pedido.IdCliente);

            if (cliente == null)
                return BadRequest("El cliente no existe.");

            existe.IdCliente = pedido.IdCliente;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un pedido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
                return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}