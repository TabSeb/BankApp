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
    public class SolicitudPrestamosController : Controller
    {
        private readonly BankContext _context;

        public SolicitudPrestamosController(BankContext context)
        {
            _context = context;
        }

        // GET: SolicitudPrestamos
        public async Task<IActionResult> Index()
        {
            var bankContext = _context.SolicitudPrestamos.Include(s => s.Cliente);
            return View(await bankContext.ToListAsync());
        }

        // GET: SolicitudPrestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SolicitudPrestamos == null)
            {
                return NotFound();
            }

            var solicitudPrestamo = await _context.SolicitudPrestamos
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.SolPrestamoId == id);
            if (solicitudPrestamo == null)
            {
                return NotFound();
            }

            return View(solicitudPrestamo);
        }

        // GET: SolicitudPrestamos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SolicitudPrestamos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolPrestamoId,CodigoPrestamo,FechaSolicitud,Aprobada,ClienteId")] SolicitudPrestamo solicitudPrestamo)
        {
            solicitudPrestamo.FechaSolicitud = DateTime.Now;
            solicitudPrestamo.ClienteId = (int)HttpContext.Session.GetInt32("clienteid");
            solicitudPrestamo.Cliente = (Cliente?)_context.Clientes.Where(x => x.IdCliente == solicitudPrestamo.ClienteId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                _context.Add(solicitudPrestamo);
                await _context.SaveChangesAsync();
                if (solicitudPrestamo.Cliente is PersonaFisica)
                {
                    return RedirectToAction("OverviewFisica", "Home", new { id = solicitudPrestamo.ClienteId });

                }
                else
                {
                    return RedirectToAction("OverviewJuridica", "Home", new { id = solicitudPrestamo.ClienteId });

                }
                
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona", solicitudPrestamo.ClienteId);
            return View(solicitudPrestamo);
        }

        // GET: SolicitudPrestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SolicitudPrestamos == null)
            {
                return NotFound();
            }

            var solicitudPrestamo = await _context.SolicitudPrestamos.FindAsync(id);
            if (solicitudPrestamo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "IdCliente", "TipoPersona", solicitudPrestamo.ClienteId);
            return View(solicitudPrestamo);
        }

        // POST: SolicitudPrestamos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolPrestamoId,CodigoPrestamo,FechaSolicitud,Aprobada,ClienteId")] SolicitudPrestamo solicitudPrestamo)
        {
            if (id != solicitudPrestamo.SolPrestamoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudPrestamo);
                    await _context.SaveChangesAsync();
                    if (solicitudPrestamo.Aprobada)
                    {
                        Prestamo prestamo = new Prestamo();
                        prestamo.ClienteId = (int)HttpContext.Session.GetInt32("clienteid");
                        prestamo.Nombre = solicitudPrestamo.CodigoPrestamo;
                        Random rand = new Random();
                        prestamo.esPrendario = (rand.Next(2) == 2);
                        _context.Prestamo.Add(prestamo);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudPrestamoExists(solicitudPrestamo.SolPrestamoId))
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
            return View(solicitudPrestamo);
        }

        // GET: SolicitudPrestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SolicitudPrestamos == null)
            {
                return NotFound();
            }

            var solicitudPrestamo = await _context.SolicitudPrestamos
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.SolPrestamoId == id);
            if (solicitudPrestamo == null)
            {
                return NotFound();
            }

            return View(solicitudPrestamo);
        }

        // POST: SolicitudPrestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SolicitudPrestamos == null)
            {
                return Problem("Entity set 'BankContext.SolicitudPrestamos'  is null.");
            }
            var solicitudPrestamo = await _context.SolicitudPrestamos.FindAsync(id);
            if (solicitudPrestamo != null)
            {
                _context.SolicitudPrestamos.Remove(solicitudPrestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudPrestamoExists(int id)
        {
          return (_context.SolicitudPrestamos?.Any(e => e.SolPrestamoId == id)).GetValueOrDefault();
        }
    }
}
