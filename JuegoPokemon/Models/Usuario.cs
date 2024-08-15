using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Name { get; set; } = "";

        [MaxLength(255)]
        public string Rol { get; set; } = "";

        [MaxLength(255)]
        public string usuario { get; set; } = "";

        [MaxLength(255)]
        public string contrasena { get; set; } = "";


        [MaxLength(255)]
        public string ConfirmarContrasena { get; set; } = "";

    }
}
