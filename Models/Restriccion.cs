using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace BancoApp.Models
{
    public class Restriccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestriccionId { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
