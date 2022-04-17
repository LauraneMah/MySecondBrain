using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySecondBrain.Infrastructure.DB;

namespace MySecondBrain.MVCApp.Controllers
{
    public class DossiersController : Controller
    {
        private readonly MySecondBrainContext _context;

        public DossiersController(MySecondBrainContext context)
        {
            _context = context;
        }

        // GET: Dossiers
        public async Task<IActionResult> Index()
        {
            var mySecondBrainContext = _context.Dossiers.Include(d => d.IddossierParentNavigation).Include(d => d.User);
            return View(await mySecondBrainContext.ToListAsync());
        }

        // GET: Dossiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossier = await _context.Dossiers
                .Include(d => d.IddossierParentNavigation)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Iddossier == id);
            if (dossier == null)
            {
                return NotFound();
            }

            return View(dossier);
        }

        // GET: Dossiers/Create
        public IActionResult Create()
        {
            ViewData["IddossierParent"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Dossiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iddossier,Nom,IddossierParent,UserId")] Dossier dossier)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dossier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IddossierParent"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", dossier.IddossierParent);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", dossier.UserId);
            return View(dossier);
        }

        // GET: Dossiers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossier = await _context.Dossiers.FindAsync(id);
            if (dossier == null)
            {
                return NotFound();
            }
            ViewData["IddossierParent"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", dossier.IddossierParent);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", dossier.UserId);
            return View(dossier);
        }

        // POST: Dossiers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddossier,Nom,IddossierParent,UserId")] Dossier dossier)
        {
            if (id != dossier.Iddossier)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dossier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DossierExists(dossier.Iddossier))
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
            ViewData["IddossierParent"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", dossier.IddossierParent);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", dossier.UserId);
            return View(dossier);
        }

        // GET: Dossiers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dossier = await _context.Dossiers
                .Include(d => d.IddossierParentNavigation)
                .Include(d => d.User)
                .FirstOrDefaultAsync(m => m.Iddossier == id);
            if (dossier == null)
            {
                return NotFound();
            }

            return View(dossier);
        }

        // POST: Dossiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dossier = await _context.Dossiers.FindAsync(id);
            _context.Dossiers.Remove(dossier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DossierExists(int id)
        {
            return _context.Dossiers.Any(e => e.Iddossier == id);
        }
    }
}
