using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public class SolicitudPaquete
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SolPaqueteId { get; set; }
        public string CodigoPaquete { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public bool Aprobada { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public string? MotivoRechazo { get; set; }

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

    }
}
