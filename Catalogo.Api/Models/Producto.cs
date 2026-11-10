namespace Catalogo.Api.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public int IdMarca { get; set; }

        public int IdCategoria { get; set; }
    }
}