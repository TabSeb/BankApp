using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoApp.Models
{
    public abstract class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }

        public string? tipoDocumento { get; set; }

        [Required]
        public int? numeroDocumento { get; set; }

        public string? direccion { get; set; }

        public Restriccion? Restriccion { get; set; }

        public List<SolicitudPrestamo>? SolicitudPrestamos { get; set; }

        public List<SolicitudPaquete>? SolicitudPaquetes { get; set; }

        public List<Producto>? Producto { get; set; }

    }

}
