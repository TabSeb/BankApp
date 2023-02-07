using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public abstract class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductoId { get; set; }

        public string? Nombre { get; set; }
        public string? descripcion { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }

}
