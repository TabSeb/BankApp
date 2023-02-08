using BancoApp.Models;
using BankApp.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly BankContext _context;

        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BankContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            

            return View();
        }


        [HttpPost]
        public IActionResult Index(PersonaJuridica modelJuridica, PersonaFisica modelFisica)//login
        {

            if (ModelState.IsValid)
            {
                var User = from m in _context.Clientes select m;
                User = User.Include(p => p.Restriccion).Where(s => s.numeroDocumento == modelJuridica.numeroDocumento);
                if (User.Count() != 0 && (User.First().Restriccion == null))
                {
                    if (User.First().numeroDocumento == modelJuridica.numeroDocumento)
                    {
                        HttpContext.Session.SetInt32("clienteid", User.First().IdCliente);
                        if (User.First() is PersonaFisica)
                        {
                            var fisica = User.First() as PersonaFisica;
                            return RedirectToAction("OverviewFisica", fisica);
                        }
                        else if (User.First() is PersonaJuridica)
                        {
                            var juridica = User.First() as PersonaJuridica;
                            return RedirectToAction("OverviewJuridica", juridica);
                        }
                    }
                }
            }
            return RedirectToAction("Error");
        }

        public IActionResult OverviewFisica(PersonaFisica fisica, int? Id)
        {
            if(Id != null)
            {
                fisica = _context.PersonaFisica.Where(x => x.IdCliente == Id).FirstOrDefault();
                
            }
            HttpContext.Session.SetInt32("clienteid", fisica.IdCliente);
            return View(fisica);
        }

        public IActionResult OverviewJuridica(PersonaJuridica juridica, int? Id)
        {
            if (Id != null)
            {
                juridica = _context.PersonaJuridica.Where(x => x.IdCliente == Id).FirstOrDefault();
            }
            HttpContext.Session.SetInt32("clienteid", juridica.IdCliente);

            return View(juridica);
        }



        public IActionResult Privacy()
        {
            return View();
        }

         [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}