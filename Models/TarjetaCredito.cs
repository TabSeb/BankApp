using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public class TarjetaCredito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TarjetaId { get; set; }
        public string? descripcion { get; set; }
        public decimal limiteCredito { get; set; }

        public int ProductoId { get; set; }
        public Paquete? Paquete { get; set; }
    }

}
