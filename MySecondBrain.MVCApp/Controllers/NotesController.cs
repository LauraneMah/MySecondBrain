using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySecondBrain.Application.Services;
using MySecondBrain.Infrastructure.DB;
using MySecondBrain.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using MySecondBrain.MVCApp.Controllers;
using MySecondBrain.MVCApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace MySecondBrain.MVCApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly MySecondBrain_LMContext _context;

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            NoteControllerService noteListControllerService = new NoteControllerService();
            var NotesList = noteListControllerService.GetNotesListViewModel();

            return View(NotesList);
        }

        // GET: Notes/Details/5
        public IActionResult Detail(int id)
        {
            var vm = Application.Services.NoteControllerService.GetNoteDetail(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }
        
        // GET: Notes/Create
        public IActionResult Create()
        {
            NoteDetailViewModel vm = new NoteDetailViewModel();

            return View();
        }

        public IActionResult Edit(int id)
        {
            var vm = Application.Services.NoteControllerService.GetNoteDetail(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(Application.ViewModels.NoteDetailViewModel noteDetailViewModel)
        {
            Application.Services.NoteControllerService.EditNote(noteDetailViewModel.Note);
            return View();
        }

        public IActionResult Delete(int id)
        {
            Application.Services.NoteControllerService.DeleteNote(id);
            return RedirectToAction("Index");
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult PostCreate(NoteDetailViewModel noteDetailViewModel)
        {
            NoteListViewModel vm = new NoteListViewModel();

            NoteControllerService.CreateNote(noteDetailViewModel.Note, this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var notes = NoteControllerService.GetNotesListOfUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            vm.Notes = notes;

            return View("Index", vm);
        }

        // GET: Notes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var note = await _context.Notes.FindAsync(id);
        //    if (note == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", note.Iddossier);
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", note.UserId);
        //    return View(note);
        //}

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Idnote,Iddossier,Titre,Description,ContenuMarkdown,DateCreation,UserId")] Note note)
        //{
        //    if (id != note.Idnote)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(note);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NoteExists(note.Idnote))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Iddossier"] = new SelectList(_context.Dossiers, "Iddossier", "Iddossier", note.Iddossier);
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", note.UserId);
        //    return View(note);
        //}

        // GET: Notes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var note = await _context.Notes
        //        .Include(n => n.IddossierNavigation)
        //        .Include(n => n.User)
        //        .FirstOrDefaultAsync(m => m.Idnote == id);
        //    if (note == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(note);
        //}

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
