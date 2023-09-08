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
    public class NacinPripremeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NacinPripremeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NacinPripreme
        public async Task<IActionResult> Index()
        {
            return View(await _context.NacinPripreme.ToListAsync());
        }

        // GET: NacinPripreme/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPripreme = await _context.NacinPripreme
                .FirstOrDefaultAsync(m => m.id == id);
            if (nacinPripreme == null)
            {
                return NotFound();
            }

            return View(nacinPripreme);
        }

        // GET: NacinPripreme/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NacinPripreme/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,listaSastojaka,opisPripreme")] NacinPripreme nacinPripreme)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nacinPripreme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nacinPripreme);
        }

        // GET: NacinPripreme/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPripreme = await _context.NacinPripreme.FindAsync(id);
            if (nacinPripreme == null)
            {
                return NotFound();
            }
            return View(nacinPripreme);
        }

        // POST: NacinPripreme/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,listaSastojaka,opisPripreme")] NacinPripreme nacinPripreme)
        {
            if (id != nacinPripreme.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nacinPripreme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NacinPripremeExists(nacinPripreme.id))
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
            return View(nacinPripreme);
        }

        // GET: NacinPripreme/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nacinPripreme = await _context.NacinPripreme
                .FirstOrDefaultAsync(m => m.id == id);
            if (nacinPripreme == null)
            {
                return NotFound();
            }

            return View(nacinPripreme);
        }

        // POST: NacinPripreme/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nacinPripreme = await _context.NacinPripreme.FindAsync(id);
            _context.NacinPripreme.Remove(nacinPripreme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NacinPripremeExists(string id)
        {
            return _context.NacinPripreme.Any(e => e.id == id);
        }
    }
}
