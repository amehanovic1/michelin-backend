using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Michelin.Data;
using Michelin.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Michelin.Util;
using Microsoft.AspNetCore.Authorization;

namespace Michelin.Controllers
{
    public class KorisnikController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;


        public KorisnikController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Korisnik
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> NadjiKorisnika()
        {
            return View(await _context.Korisnik.ToListAsync());
        }

        [Authorize(Roles ="Korisnik")]
        public IActionResult Deaktivacija()
        {
            return View();
        }

        
        [Authorize(Roles = "Korisnik")]
        public IActionResult Obavijesti()
        {
            return View();
        }

        [Authorize(Roles = "Korisnik")]
        public IActionResult Pomoc()
        {
            return View();
        }
        // GET: Korisnik/Details/5
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Profil()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            Korisnik korisnik = await _userManager.GetUserAsync(User);
            ViewBag.recepti = await _context.Recept.ToListAsync();
            ViewBag.korisnici = await _context.Korisnik.ToListAsync();
            ViewBag.ocjene = await _context.Ocjena.ToListAsync();
            return View(korisnik);
        }

        public async Task<IActionResult> Details(string id)
        {
            //var id = User.FindFirst(ClaimTypes.NameIdentifier);
            //Korisnik korisnik = await _userManager.GetUserAsync(User);
            Korisnik korisnik = _context.Korisnik.Find(id);
            ViewBag.recepti = await _context.Recept.ToListAsync();
            ViewBag.korisnici = await _context.Korisnik.ToListAsync();
            ViewBag.ocjene = await _context.Ocjena.ToListAsync();
            return View("Profil",korisnik);
        }

        // GET: Korisnik/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korisnik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(korisnik);
        }

        // GET: Korisnik/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);

            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Uredi(Korisnik korisnik)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korisnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorisnikExists(korisnik.korisnickoIme))
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
            else 
            {
                return View(korisnik);
            }
        }



        // GET: Korisnik/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(m => m.korisnickoIme == id);
            if (korisnik == null)
            {
                return NotFound();
            }

            return View(korisnik);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            _context.Korisnik.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction("NadjiKorisnika");
        }

        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> Obrisi(string id)
        {
            var korisnik = await _context.Korisnik.FindAsync(id);
            _context.Korisnik.Remove(korisnik);
            await _context.SaveChangesAsync();
            return RedirectToAction("NadjiKorisnika");

        }


        private bool KorisnikExists(string id)
        {
            return _context.Korisnik.Any(e => e.korisnickoIme == id);
        }

        public async Task<IActionResult> ukljuciObavijesti()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            Korisnik korisnik = await _userManager.GetUserAsync(User);
            korisnik.ukljuciObavijesti();

            return RedirectToAction("Obavijesti", "Korisnik");
            
        }

        public async Task<IActionResult> iskljuciObavijesti()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier);
            Korisnik korisnik = await _userManager.GetUserAsync(User);
            korisnik.iskljuciObavijesti();

            return RedirectToAction("Obavijesti", "Korisnik");
            
        }


    }
}
