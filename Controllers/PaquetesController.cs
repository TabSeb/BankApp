using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BancoApp.Models;

namespace BankApp.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly BankContext _context;

        public PaquetesController(BankContext context)
        {
            _context = context;
        }

        // GET: Paquetes
        public async Task<IActionResult> Index()
        {
            var bankContext = _context.Paquete.Include(p => p.Cliente).Where(p => p.ClienteId == (int)HttpContext.Session.GetInt32("clienteid"));
            return View(await bankContext.ToListAsync());

        }

        // GET: Paquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Paquete == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquete
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: Paquetes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona");
            return View();
        }

        // POST: Paquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("esCrediticio,TarjetasId,ProductoId,Nombre,descripcion,ClienteId")] Paquete paquete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paquete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona", paquete.ClienteId);
            return View(paquete);
        }

        // GET: Paquetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Paquete == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquete.FindAsync(id);
            if (paquete == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona", paquete.ClienteId);
            return View(paquete);
        }

        // POST: Paquetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("esCrediticio,TarjetasId,ProductoId,Nombre,descripcion,ClienteId")] Paquete paquete)
        {
            if (id != paquete.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.ProductoId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona", paquete.ClienteId);
            return View(paquete);
        }

        // GET: Paquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Paquete == null)
            {
                return NotFound();
            }

            var paquete = await _context.Paquete
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // POST: Paquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Paquete == null)
            {
                return Problem("Entity set 'BankContext.Paquete'  is null.");
            }
            var paquete = await _context.Paquete.FindAsync(id);
            if (paquete != null)
            {
                _context.Paquete.Remove(paquete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
          return (_context.Paquete?.Any(e => e.ProductoId == id)).GetValueOrDefault();
        }
    }
}
