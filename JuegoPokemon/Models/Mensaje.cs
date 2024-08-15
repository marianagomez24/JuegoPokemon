using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class Mensaje
    {
        public int Id { get; set; }

        [MaxLength(500)]
        public string Contenido { get; set; } = "";

        public int Id_Remitente { get; set; }

        public int Id_Destinatario { get; set; }

        public DateTime FechaEnvio { get; set; }

    }
}
