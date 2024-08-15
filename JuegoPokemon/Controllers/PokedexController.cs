using JuegoPokemon.Models;
using JuegoPokemon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JuegoPokemon.Controllers
{
    [Authorize]
    public class PokedexController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PokedexController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var pokemons = await _applicationDbContext.Pokemons.ToListAsync();
            return View(pokemons);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Crear(Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Pokemons.Add(pokemon);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pokemon);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id)
        {
            var pokemon = await _applicationDbContext.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id, Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _applicationDbContext.Update(pokemon);
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_applicationDbContext.Pokemons.Any(e => e.Id == pokemon.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(pokemon);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var pokemon = await _applicationDbContext.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        [HttpPost, ActionName("Eliminar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var pokemon = await _applicationDbContext.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _applicationDbContext.Pokemons.Remove(pokemon);
                await _applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detalles(int id)
        {
            var pokemon = await _applicationDbContext.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }
    }
}
