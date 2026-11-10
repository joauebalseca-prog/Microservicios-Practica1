namespace Pedidos.Api.Models
{
    public class DetallePedido
    {
        public int IdDetallePedido { get; set; }

        public int IdPedido { get; set; }

        public int IdProducto { get; set; }
    }
}