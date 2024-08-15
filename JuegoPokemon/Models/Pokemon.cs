using System.ComponentModel.DataAnnotations;

namespace JuegoPokemon.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nombre { get; set; } = "";

        [MaxLength(255)]
        public string Tipo { get; set; } = "";

        [MaxLength(255)]
        public string Debilidad { get; set; } = "";

        [MaxLength(255)]
        public string Evoluciones { get; set; } = "";

        public int Peso { get; set; }

        public int Numero { get; set; }

        public int Id_Entrenador { get; set; }
    }
}
