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
    public class SolicitudPaquetesController : Controller
    {
        private readonly BankContext _context;

        public SolicitudPaquetesController(BankContext context)
        {
            _context = context;
        }

        // GET: SolicitudPaquetes
        public async Task<IActionResult> Index()
        {
              return _context.SolicitudPaquetes != null ? 
                          View(await _context.SolicitudPaquetes.ToListAsync()) :
                          Problem("Entity set 'BankContext.SolicitudPaquetes'  is null.");
        }

        // GET: SolicitudPaquetes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SolicitudPaquetes == null)
            {
                return NotFound();
            }

            var solicitudPaquete = await _context.SolicitudPaquetes
                .FirstOrDefaultAsync(m => m.SolPaqueteId == id);
            if (solicitudPaquete == null)
            {
                return NotFound();
            }

            return View(solicitudPaquete);
        }

        // GET: SolicitudPaquetes/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: SolicitudPaquetes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SolPaqueteId,CodigoPaquete")] SolicitudPaquete solicitudPaquete)
        {
            solicitudPaquete.FechaSolicitud = DateTime.Now;
            solicitudPaquete.ClienteId= (int)HttpContext.Session.GetInt32("clienteid");
            solicitudPaquete.Cliente = (Cliente?)_context.Clientes.Where(x => x.IdCliente == solicitudPaquete.ClienteId).FirstOrDefault();


            if (ModelState.IsValid)
            {
                _context.Add(solicitudPaquete);
                await _context.SaveChangesAsync();

                if(solicitudPaquete.Cliente is PersonaFisica)
                {
                    return RedirectToAction("OverviewFisica", "Home", new { id = solicitudPaquete.ClienteId });

                }
                else
                {
                    return RedirectToAction("OverviewJuridica", "Home", new { id = solicitudPaquete.ClienteId });

                }

            }
            return View(solicitudPaquete);
        }

        // GET: SolicitudPaquetes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SolicitudPaquetes == null)
            {
                return NotFound();
            }

            var solicitudPaquete = await _context.SolicitudPaquetes.FindAsync(id);
            if (solicitudPaquete == null)
            {
                return NotFound();
            }
            return View(solicitudPaquete);
        }

        // POST: SolicitudPaquetes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SolPaqueteId,CodigoPaquete,FechaSolicitud,Aprobada,FechaAprobacion,MotivoRechazo,ClienteId")] SolicitudPaquete solicitudPaquete)
        {
            if (id != solicitudPaquete.SolPaqueteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solicitudPaquete);
                    await _context.SaveChangesAsync();

                    if (solicitudPaquete.Aprobada)
                    {
                        Paquete paquete = new Paquete();
                        Random rand = new Random();
                        paquete.esCrediticio = (rand.Next(2) == 2);
                        paquete.ClienteId = (int)HttpContext.Session.GetInt32("clienteid");
                        _context.Paquete.Add(paquete);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolicitudPaqueteExists(solicitudPaquete.SolPaqueteId))
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
            return View(solicitudPaquete);
        }

        // GET: SolicitudPaquetes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SolicitudPaquetes == null)
            {
                return NotFound();
            }

            var solicitudPaquete = await _context.SolicitudPaquetes
                .FirstOrDefaultAsync(m => m.SolPaqueteId == id);
            if (solicitudPaquete == null)
            {
                return NotFound();
            }

            return View(solicitudPaquete);
        }

        // POST: SolicitudPaquetes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SolicitudPaquetes == null)
            {
                return Problem("Entity set 'BankContext.SolicitudPaquetes'  is null.");
            }
            var solicitudPaquete = await _context.SolicitudPaquetes.FindAsync(id);
            if (solicitudPaquete != null)
            {
                _context.SolicitudPaquetes.Remove(solicitudPaquete);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolicitudPaqueteExists(int id)
        {
          return (_context.SolicitudPaquetes?.Any(e => e.SolPaqueteId == id)).GetValueOrDefault();
        }
    }
}
