using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class Permiso
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nombre_Permiso { get; set; } = "";
    }
}
