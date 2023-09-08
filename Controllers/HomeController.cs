using Michelin.Data;
using Michelin.Models;
using Michelin.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Michelin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> SviRecepti()
        {
            ViewBag.korisnici = await _context.Korisnik.ToListAsync();
            ViewBag.ocjene = await _context.Ocjena.ToListAsync();
            return View(await _context.Recept.ToListAsync());
        }

        public async Task<IActionResult> NajboljiRecepti()
        {
            /* azurirajNajboljeRecepte();
             var najboljiRecepti = await _context.NajboljiRecept.ToListAsync();
             List<Recept> recepti = new List<Recept>();
             foreach(NajboljiRecept r in najboljiRecepti)
             {
                 recepti.Add(r.recept);
             }*/

            ViewBag.ocjene = await _context.Ocjena.ToListAsync();
            ViewBag.korisnici = await _context.Korisnik.ToListAsync();
            return View(Recept.DajDesetNajboljihRecepata(await _context.Recept.ToListAsync())); 
        }


        public async void azurirajNajboljeRecepte()
        {
            var recepti = Recept.DajDesetNajboljihRecepata(await _context.Recept.ToListAsync());
            var najbolji = await _context.NajboljiRecept.ToListAsync();

            if (najbolji.Count<10)
            {
                foreach(Recept r in recepti)
                {
                    var novi = new NajboljiRecept();
                    novi.id = r.id;
                    novi.recept = r;

                    _context.SaveChanges();
                }

            } else
            {
                int i = 0;
                foreach(NajboljiRecept r in najbolji)
                {
                    r.recept = recepti[i];
                    _context.Update(r);
                    i++;
                }
            }

            //await _context.SaveChangesAsync();
            PretplatnikRepozitorij.getInstance().obavijestiPretplatnike();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
