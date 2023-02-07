using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public class Paquete : Producto
    {
        

        
        public bool esCrediticio { get; set; }

        public int? TarjetasId { get; set; }
        public List<TarjetaCredito>? tarjetas { get; set; } //maximo 3 tarjetas
    }
}
