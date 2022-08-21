using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySecondBrain.Application.Services;
using MySecondBrain.Infrastructure.DB;
using MySecondBrain.Application.ViewModels;
using System.Security.Claims;

namespace MySecondBrain.MVCApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly MySecondBrain_LMContext _context;

        // GET: Notes
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            NoteControllerService noteListControllerService = new NoteControllerService();
            var NotesList = noteListControllerService.GetNotesListViewModel(userId);

            return View(NotesList);            
        }

        // GET: Notes/Details/5
        public IActionResult Detail(int id)
        {
            var vm = NoteControllerService.GetNoteDetail(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }
        
        // GET: Notes/Create
        public IActionResult Create()
        {
            NoteDetailViewModel vm = NoteControllerService.GetNoteDetail();
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var vm = NoteControllerService.GetNoteDetail(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(NoteDetailViewModel noteDetailViewModel)
        {
            NoteControllerService.EditNote(noteDetailViewModel.Note);
            return View();
        }

        public IActionResult Delete(int id)
        {
            NoteControllerService.DeleteNote(id);
            return RedirectToAction("Index");
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult PostCreate(NoteDetailViewModel noteDetailViewModel)
        {

            int idDossier = noteDetailViewModel.IDDossier;

            NoteControllerService.CreateNote(noteDetailViewModel.Note, this.User.FindFirstValue(ClaimTypes.NameIdentifier), idDossier );


            return RedirectToAction("Index");
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
