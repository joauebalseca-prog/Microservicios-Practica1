using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly CatalogoDBContext _context;

        public ProductosController(CatalogoDBContext context)
        {
            _context = context;
        }

        // Consultar todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // Consultar un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            return producto;
        }

        // Registrar un producto
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            var marca = await _context.Marcas.FindAsync(producto.IdMarca);
            var categoria = await _context.Categorias.FindAsync(producto.IdCategoria);

            if (marca == null || categoria == null)
                return BadRequest("La marca o categoría no existe.");

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto),
                new { id = producto.IdProducto }, producto);
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.IdProducto)
                return BadRequest();

            var existe = await _context.Productos.FindAsync(id);

            if (existe == null)
                return NotFound();

            var marca = await _context.Marcas.FindAsync(producto.IdMarca);
            var categoria = await _context.Categorias.FindAsync(producto.IdCategoria);

            if (marca == null || categoria == null)
                return BadRequest("La marca o categoría no existe.");

            existe.Nombre = producto.Nombre;
            existe.IdMarca = producto.IdMarca;
            existe.IdCategoria = producto.IdCategoria;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}