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
    public class NotesController : Controller
    {
        private readonly MySecondBrainContext _context;

        public NotesController(MySecondBrainContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var mySecondBrainContext = _context.Notes.Include(n => n.IddossierNavigation).Include(n => n.User);
            return View(await mySecondBrainContext.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.IddossierNavigation)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Idnote == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idnote,Iddossier,Titre,Description,ContenuMarkdown,DateCreation,UserId")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", note.Iddossier);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", note.UserId);
            return View(note);
        }

        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", note.Iddossier);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", note.UserId);
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idnote,Iddossier,Titre,Description,ContenuMarkdown,DateCreation,UserId")] Note note)
        {
            if (id != note.Idnote)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Idnote))
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
            ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", note.Iddossier);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", note.UserId);
            return View(note);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notes
                .Include(n => n.IddossierNavigation)
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Idnote == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Idnote == id);
        }
    }
}
