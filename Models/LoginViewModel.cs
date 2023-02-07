using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankApp.Models
{
    public class LoginViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCliente { get; set; }
        public string TipoCliente { get; set; }

        public int NumeroDocumento { get; set; }

        public string TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public string? RazonSocial { get; set; }

        public string Direccion { get; set; }
    }
}
