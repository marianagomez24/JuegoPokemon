using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class PeticionEnfermeria
    {
        public int Id { get; set; }

        public int Id_Entrenador { get; set; }

        public int Id_Pokemon { get; set; }

        public int Id_Enfermero { get; set; }

        [MaxLength(255)]
        public string EstadoPokemon { get; set; } = "";


    }
}
