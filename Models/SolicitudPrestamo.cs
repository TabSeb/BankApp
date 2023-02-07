using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public class SolicitudPrestamo //personaFisica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolPrestamoId { get; set; }
        public string CodigoPrestamo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool Aprobada { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
