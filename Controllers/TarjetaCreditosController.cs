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
    public class TarjetaCreditosController : Controller
    {
        private readonly BankContext _context;

        public TarjetaCreditosController(BankContext context)
        {
            _context = context;
        }

        // GET: TarjetaCreditos
        public async Task<IActionResult> Index()
        {
            var paquetes = _context.Paquete.Where(p => p.ClienteId == (int)HttpContext.Session.GetInt32("clienteid"));
            var tarjetas = _context.tarjetaCreditos.Include(p => p.Paquete).Where(p => p.ProductoId == paquetes.FirstOrDefault().ProductoId);

            return View(await tarjetas.ToListAsync());
        }

        // GET: TarjetaCreditos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tarjetaCreditos == null)
            {
                return NotFound();
            }

            var tarjetaCredito = await _context.tarjetaCreditos
                .FirstOrDefaultAsync(m => m.TarjetaId == id);
            if (tarjetaCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaCredito);
        }

        // GET: TarjetaCreditos/Create
        public IActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;

            return View();
        }

        // POST: TarjetaCreditos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TarjetaId,descripcion,limiteCredito,ProductoId")] TarjetaCredito tarjetaCredito)
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(tarjetaCredito);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Paquetes");
            }
            return View(tarjetaCredito);
        }

        // GET: TarjetaCreditos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tarjetaCreditos == null)
            {
                return NotFound();
            }

            var tarjetaCredito = await _context.tarjetaCreditos.FindAsync(id);
            if (tarjetaCredito == null)
            {
                return NotFound();
            }
            return View(tarjetaCredito);
        }

        // POST: TarjetaCreditos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TarjetaId,descripcion,limiteCredito,ProductoId")] TarjetaCredito tarjetaCredito)
        {
            if (id != tarjetaCredito.TarjetaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarjetaCredito);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarjetaCreditoExists(tarjetaCredito.TarjetaId))
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
            return View(tarjetaCredito);
        }

        // GET: TarjetaCreditos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tarjetaCreditos == null)
            {
                return NotFound();
            }

            var tarjetaCredito = await _context.tarjetaCreditos
                .FirstOrDefaultAsync(m => m.TarjetaId == id);
            if (tarjetaCredito == null)
            {
                return NotFound();
            }

            return View(tarjetaCredito);
        }

        // POST: TarjetaCreditos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tarjetaCreditos == null)
            {
                return Problem("Entity set 'BankContext.tarjetaCreditos'  is null.");
            }
            var tarjetaCredito = await _context.tarjetaCreditos.FindAsync(id);
            if (tarjetaCredito != null)
            {
                _context.tarjetaCreditos.Remove(tarjetaCredito);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarjetaCreditoExists(int id)
        {
          return (_context.tarjetaCreditos?.Any(e => e.TarjetaId == id)).GetValueOrDefault();
        }
    }
}
