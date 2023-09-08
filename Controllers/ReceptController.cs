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
using Microsoft.AspNetCore.Http;
using Michelin.Interfaces;
using Michelin.Util;
using Michelin.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.AspNetCore.Authorization;

namespace Michelin.Controllers
{
    public class ReceptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;
        private List<Sastojak> sastojci;
        public ReceptController(ApplicationDbContext context, UserManager<Korisnik> userManager, IUnitOfWork unitOfWork)
        {
            _context = context;
            _userManager = userManager;
            sastojci = new List<Sastojak>();
            _unitOfWork = unitOfWork;
        }

        

        // GET: Recepts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept
                .FirstOrDefaultAsync(m => m.id == id);
            if (recept == null)
            {
                return NotFound();
            }

            List<Korisnik> korisnici = await _context.Korisnik.ToListAsync();
            var korisnik = korisnici.Find(k => recept.autor == k);
            List<NacinPripreme> nacini = await _context.NacinPripreme.ToListAsync();
            var nacinPripreme = nacini.Find(n => recept.nacinPripreme == n);
            Korisnik trenutni = await _userManager.GetUserAsync(User);

            ViewBag.korisnik = korisnik;
            ViewBag.nacin = nacinPripreme.opisPripreme;
            ViewBag.ocjene = await _context.Ocjena.ToListAsync();
            ViewBag.trenutni = trenutni;

            return View(recept);
        }

        // GET: Recepts/Create
        [Authorize(Roles = "Korisnik")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Create(IFormFile file,IFormCollection form,[Bind("naziv,vrijemePripreme,nacionalnoJelo,vrstaJela,vegansko")] Recept recept)
        {

            /*if (ModelState.IsValid)
            {
                if(file==null)
                {
                    return View();
                }
                using var image = Image.Load(file.OpenReadStream());
                image.Mutate(x => x.Resize(50, 50));
                _unitOfWork.UploadImage(file);
                recept.slika = file.FileName;
                var id = User.FindFirst(ClaimTypes.NameIdentifier);
                Korisnik korisnik = await _userManager.GetUserAsync(User);
              
                recept.autor = korisnik;
                recept.datum = DateTime.Now;
                recept.nacinPripreme = new NacinPripreme();
                recept.nacinPripreme.id = recept.id;
                recept.nacinPripreme.opisPripreme = form["opis"];

                string kljucevi = "";
                foreach(Sastojak s in sastojci)
                {
                    kljucevi += s.id + " ";
                }
                recept.nacinPripreme.listaSastojaka = kljucevi;
                _context.Add(recept.nacinPripreme);
                _context.Add(recept);
                await _context.SaveChangesAsync();
                return RedirectToAction("SviRecepti","Home");
            }*/
            return RedirectToAction("SviRecepti","Home");
        }

        // GET: Recepts/Edit/5
        /* public async Task<IActionResult> Edit(string id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var recept = await _context.Recept.FindAsync(id);
             if (recept == null)
             {
                 return NotFound();
             }
             return View(recept);
         }*/

        // POST: Recepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(string id, [Bind("id,naziv,vrijemePripreme,nacionalnoJelo,vrstaJela,vegansko,datum")] Recept recept)
         {
             if (id != recept.id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(recept);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!ReceptExists(recept.id))
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
             return View(recept);
         }
        */
        // GET: Recepts/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recept = await _context.Recept
                .FirstOrDefaultAsync(m => m.id == id);
            if (recept == null)
            {
                return NotFound();
            }

            return View(recept);
        }

        // POST: Recepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recept = await _context.Recept.FindAsync(id);
            _context.Recept.Remove(recept);
            await _context.SaveChangesAsync();
            return RedirectToAction("SviRecepti","Home");
        }

        private bool ReceptExists(string id)
        {
            return _context.Recept.Any(e => e.id == id);
        }

        public async Task<IActionResult> Filtriranje()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Filtriranje(string[] str)
        {
            List<Recept> recepti = await _context.Recept.ToListAsync();
            

            return RedirectToAction("SviRecepti","Recept",recepti);
        }

        [Route("DodajSastojke/")]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> DodajSastojke(string id)
        {
            return View();
        }
        
        [HttpPost]
        [Route("DodajSastojke/")]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult>  DodajSastojke(IFormCollection form)
        {
           /* string naziv = form["nazivSastojka"].ToString();
            double kolicina = Double.Parse(form["kolicina"].ToString());
            MjernaJedinica mjerna = (MjernaJedinica)Enum.Parse(typeof(MjernaJedinica), form["mjerna"]);

            Sastojak sastojak = (Sastojak)BazaSastojaka.getInstance().dodajUBazuSastojaka(naziv, kolicina, mjerna);
            sastojci.Add(sastojak);
            string id = form["id"];
            var recept = _context.Find<Recept>(id);
            recept.nacinPripreme.listaSastojaka += sastojak.id + " ";*/
            return RedirectToAction("DodajSastojke", "Recept");
            
        }

        public async Task<IActionResult> dodajUOmiljene(string id)
        {
           
           /* Korisnik korisnik = await _userManager.GetUserAsync(User);
            Recept recept = await _context.Recept.FindAsync(id);
            var omiljeni = korisnik.pretvoriStringUListu(await _context.Recept.ToListAsync());

            if (!(omiljeni.Contains(recept)))
            {
                korisnik.dodajOmiljeniRecept(recept);
               
            }*/

            return RedirectToAction("Details", "Recept", id);
        }

        [HttpPost]
        public async Task<IActionResult> ocijeniRecept(string id, IFormCollection form)
        {
            
            string ocjena = form["ocjena"];
          
            Recept recept = await _context.Recept.FindAsync(id);
            Korisnik korisnik = await _userManager.GetUserAsync(User);

            Double vrijednost = Double.Parse(ocjena);

            List<Ocjena> ocjene = await _context.Ocjena.ToListAsync();

            foreach(Ocjena o in ocjene)
            {
                if (o.korisnik==korisnik && o.recept==recept)
                {
                    o.vrijednost = vrijednost;
                    _context.Update(o);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("SviRecepti", "Home");
                }
            }

            _context.Add<Ocjena>(new Ocjena(korisnik, recept, vrijednost));
            await _context.SaveChangesAsync();
            return RedirectToAction("SviRecepti", "Home");
            return RedirectToAction("Details", "Recept", id);
        }
    }
}
