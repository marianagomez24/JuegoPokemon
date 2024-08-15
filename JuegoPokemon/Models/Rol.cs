using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class Rol
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nombre_Rol { get; set; } = "";
    }
}
