using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BancoApp.Models;
using BankApp.Models;

namespace BankApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly BankContext _context;

        public ClienteController(BankContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            var bankContext = _context.Clientes.Include(c => c.Restriccion);
            return View(await bankContext.ToListAsync());
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Restriccion)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            ViewData["RestriccionId"] = new SelectList(_context.Restricciones, "RestriccionId", "RestriccionId");
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,TipoCliente,TipoDocumento,NumeroDocumento,Nombre,Apellido,RazonSocial,Direccion ")] LoginViewModel cliente)
        {
            


            if (ModelState.IsValid)
            {
                if(cliente.TipoCliente == "Fisica")
                {
                    PersonaFisica personaFisica = new PersonaFisica
                    {
                        nombre = cliente.Nombre,
                        apellido = cliente.Apellido,
                        tipoDocumento = cliente.TipoDocumento,
                        numeroDocumento = cliente.NumeroDocumento,
                        direccion = cliente.Direccion

                    };
                    _context.Add(personaFisica);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("OverviewFisica", "Home", new {id = personaFisica.IdCliente});

                }
                else
                {
                    PersonaJuridica personaJuridica = new PersonaJuridica
                    {
                        razonSocial = cliente.RazonSocial,
                        tipoDocumento = cliente.TipoDocumento,
                        numeroDocumento = cliente.NumeroDocumento,
                        direccion = cliente.Direccion
                    };
                    _context.Add(personaJuridica);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("OverviewJuridica", "Home", new { id = personaJuridica.IdCliente });

                }

                await _context.SaveChangesAsync();
            }
            //ViewData["RestriccionId"] = new SelectList(_context.Restricciones, "RestriccionId", "RestriccionId", cliente.RestriccionId);
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            //ViewData["RestriccionId"] = new SelectList(_context.Restricciones, "RestriccionId", "RestriccionId", cliente.RestriccionId);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,tipoDocumento,numeroDocumento,RestriccionId,SolPaqueteId,SolPrestamoId,ProductoId")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.IdCliente))
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
            //ViewData["RestriccionId"] = new SelectList(_context.Restricciones, "RestriccionId", "RestriccionId", cliente.RestriccionId);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clientes == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Restriccion)
                .FirstOrDefaultAsync(m => m.IdCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'BankContext.Clientes'  is null.");
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
          return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
