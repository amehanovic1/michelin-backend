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
using Michelin.Interfaces;
using Michelin.Util;
using Microsoft.AspNetCore.Authorization;

namespace Michelin.Controllers
{
    public class ZahtjeviZaPomocController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ZahtjeviZaPomocController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ZahtjeviZaPomoc
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var zahtjevi = await _context.ZahtjevZaPomoc.ToListAsync();
    
            zahtjevi.RemoveAll((z) => z.obradjeno == true);

            IIterator iterator = new PrioritetniIterator(zahtjevi);

            var zahtjeviSortirano = new List<ZahtjevZaPomoc>();
            var broj = zahtjevi.Count;

            try
            {
                while (broj > 0)
                {
                    zahtjeviSortirano.Add(iterator.dajNaredniZahtjevZaPomoc());
                    broj--;
                }
            }
            catch (Exception) { }
            return View(zahtjeviSortirano);
        }



        // GET: ZahtjeviZaPomoc/Details/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaPomoc = await _context.ZahtjevZaPomoc
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaPomoc == null)
            {
                return NotFound();
            }

            return View(zahtjevZaPomoc);
        }

        // GET: ZahtjeviZaPomoc/Create
        [Authorize]
        [Authorize(Roles = "Korisnik")]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: ZahtjeviZaPomoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("kategorija,sadrzaj")] ZahtjevZaPomoc zahtjevZaPomoc)
        {
            if(ModelState.IsValid)
            {
                var id = User.FindFirst(ClaimTypes.NameIdentifier);
                Korisnik korisnik = await _userManager.GetUserAsync(User);
                zahtjevZaPomoc.obradjeno = false;
                zahtjevZaPomoc.korisnik = korisnik;
                zahtjevZaPomoc.datum = DateTime.Now;
                _context.Add(zahtjevZaPomoc);
                await _context.SaveChangesAsync();

                return RedirectToAction("SviRecepti", "Home");
            }

            return RedirectToAction("Greska");
        }

        [Authorize]
        [Authorize(Roles = "Administrator")]
        public async Task<RedirectToActionResult> Obradi(string id)
        {
            var zahtjevZaPomoc = await _context.ZahtjevZaPomoc.FindAsync(id);
            zahtjevZaPomoc.obradjeno = true;
            _context.Update(zahtjevZaPomoc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        // GET: ZahtjeviZaPomoc/Delete/5
        [Authorize]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zahtjevZaPomoc = await _context.ZahtjevZaPomoc
                .FirstOrDefaultAsync(m => m.id == id);
            if (zahtjevZaPomoc == null)
            {
                return NotFound();
            }

            return View(zahtjevZaPomoc);
        }

        // POST: ZahtjeviZaPomoc/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var zahtjevZaPomoc = await _context.ZahtjevZaPomoc.FindAsync(id);
            _context.ZahtjevZaPomoc.Remove(zahtjevZaPomoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZahtjevZaPomocExists(string id)
        {
            return _context.ZahtjevZaPomoc.Any(e => e.id == id);
        }
    }
}
