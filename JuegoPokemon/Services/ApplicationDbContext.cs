using JuegoPokemon.Models;
using Microsoft.EntityFrameworkCore;

namespace JuegoPokemon.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Entrenador> Entrenadores { get; set; }

        public DbSet<Pokemon> Pokemons { get; set; }

        public DbSet<Enfermero> Enfermeros { get; set; }

        public DbSet<PeticionEnfermeria> peticionEnfermerias { get; set; }

        public DbSet<Reto> Retos { get; set; }

        public DbSet<Mensaje> Mensajes { get; set; }

        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<RolePermiso> RolesPermisos  { get; set; }

    }
}
