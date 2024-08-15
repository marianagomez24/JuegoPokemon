using JuegoPokemon.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JuegoPokemon.Services;

namespace JuegoPokemon.Controllers
{
    public class AccesoController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AccesoController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario modelo)
        {
            if (modelo.contrasena != modelo.ConfirmarContrasena)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }

            Usuario usuario = new Usuario()
            {
                Name = modelo.Name,
                Rol = modelo.Rol,
                usuario = modelo.usuario,
                contrasena = modelo.contrasena,
                ConfirmarContrasena = modelo.ConfirmarContrasena
            };

            await _applicationDbContext.Usuarios.AddAsync(usuario);
            await _applicationDbContext.SaveChangesAsync();

            var rolId = await _applicationDbContext.Roles
                .Where(r => r.Nombre_Rol == usuario.Rol)
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (rolId != 0)
            {
                var permisos = new List<int>();
                if (usuario.Rol == "Admin")
                {
                    permisos = await _applicationDbContext.Permisos
                        .Where(p => p.Nombre_Permiso == "Crear" ||
                                    p.Nombre_Permiso == "Leer" ||
                                    p.Nombre_Permiso == "Actualizar" ||
                                    p.Nombre_Permiso == "Eliminar" ||
                                    p.Nombre_Permiso == "CargarPokedex")
                        .Select(p => p.Id)
                        .ToListAsync();
                }
                else if (usuario.Rol == "Enfermero" || usuario.Rol == "Entrenador")
                {
                    var permisoCargarPokedex = await _applicationDbContext.Permisos
                        .Where(p => p.Nombre_Permiso == "CargarPokedex")
                        .Select(p => p.Id)
                        .FirstOrDefaultAsync();

                    if (permisoCargarPokedex != 0)
                    {
                        permisos.Add(permisoCargarPokedex);
                    }
                }

                foreach (var permisoId in permisos)
                {
                    var rolPermiso = new RolePermiso()
                    {
                        Id_Rol = rolId,
                        Id_Permiso = permisoId,
                        Estado = true
                    };
                    await _applicationDbContext.RolesPermisos.AddAsync(rolPermiso);
                }
            }

            await _applicationDbContext.SaveChangesAsync();

            if (usuario.Id != 0)
                return RedirectToAction("Login", "Acceso");

            ViewData["Mensaje"] = "No se pudo crear el usuario.";
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario modelo)
        {
            var usuario_encontrado = await _applicationDbContext.Usuarios
                .FirstOrDefaultAsync(u => u.usuario == modelo.usuario && u.contrasena == modelo.contrasena);

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontraron coincidencias";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.Name),
                new Claim(ClaimTypes.NameIdentifier, usuario_encontrado.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario_encontrado.Rol)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}
