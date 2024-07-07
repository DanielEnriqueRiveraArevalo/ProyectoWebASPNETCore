using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoWebASPNETCore.Models;

namespace ProyectoWebASPNETCore.Controllers
{
    public class UsersController : Controller
    {
        private readonly PROYECTOWEBASPNETCOREContext _context;
        /* Se establece un constructor como parametro recive el contexto de la base de datos qeu se creo a aprtir de Scaffold*/
        public UsersController(PROYECTOWEBASPNETCOREContext context)
        {
            _context = context;
        }

        // Se obtiene a todos los usuarios de la base de datos de forma asincrona con await y ToListAsync()
        public async Task<IActionResult> Index()
        {
            try
            {
                return _context.Users != null ?
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'PROYECTOWEBASPNETCOREContext.Users'  is null.");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }
              
        }

        //  Se obtiene un usuario de la base de datos apartir de proporcionar el Id del usuario, trabaja de forma asincrona.
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }
        }

       

        // Metodo que proviene de la vista parta crear un nuevo usuario, y retornarlo a la vista.
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }
            
        }

        // Metodo para agregar un nuevo usuario a la base de datos de forma asincrona y que retorna a la vista.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Mensaje")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }

            
        }

        // Metodo para buscar al usuario ha ser editado.
        public async Task<IActionResult> Edit(int? id)
        {

            try
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }

            
        }

        // Metodo para editar el usuario validando la existencia del Id para poder ser modificado y guardado en base de datos asincronamente.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Mensaje")] User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }

            
        }

        // Metodo para buscar al usuario ha ser eliminado.
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }
 
        }

        //// Metodo para eliminar usuario validando que el usuario encontrado no sea null posterior a la eliminacion se guardan los cambio en la base de datos..
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.Users == null)
                {
                    return Problem("Entity set 'PROYECTOWEBASPNETCOREContext.Users'  is null.");
                }
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error de proceso: {ex.Message}");
            }

            
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
