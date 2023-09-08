using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Michelin.Data;
using Michelin.Models;

namespace Michelin.Controllers
{
    public class SastojakController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SastojakController(ApplicationDbContext context)
        {
            _context = context;
        }

        public 
        // GET: Sastojak
        async Task<IActionResult> Index()
        {
            return View(await _context.Sastojak.ToListAsync());
        }

        // GET: Sastojak/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sastojak = await _context.Sastojak
                .FirstOrDefaultAsync(m => m.id == id);
            if (sastojak == null)
            {
                return NotFound();
            }

            return View(sastojak);
        }

        // GET: Sastojak/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sastojak/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //Da biste se zaštitili od napada sa prekomjernim unosima podataka (overposting attacks),
        //omogućite samo određene svojstva koja želite povezati (bind) sa modelom, za više detalja
        //pogledajte http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> KreirajSastojak([Bind("id,naziv,kolicina,mjernaJedinica")] Sastojak sastojak)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sastojak);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sastojak);
        }

        // GET: Sastojak/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sastojak = await _context.Sastojak.FindAsync(id);
            if (sastojak == null)
            {
                return NotFound();
            }
            return View(sastojak);
        }

        // POST: Sastojak/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,naziv,kolicina,mjernaJedinica")] Sastojak sastojak)
        {
            if (id != sastojak.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sastojak);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SastojakExists(sastojak.id))
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
            return View(sastojak);
        }

        // GET: Sastojak/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sastojak = await _context.Sastojak
                .FirstOrDefaultAsync(m => m.id == id);
            if (sastojak == null)
            {
                return NotFound();
            }

            return View(sastojak);
        }

        // POST: Sastojak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sastojak = await _context.Sastojak.FindAsync(id);
            _context.Sastojak.Remove(sastojak);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SastojakExists(string id)
        {
            return _context.Sastojak.Any(e => e.id == id);
        }
    }
}
