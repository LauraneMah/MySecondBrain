using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySecondBrain.Infrastructure.DB;
using MySecondBrain.Application.Services;
using MySecondBrain.Application.ViewModels;
using System.Security.Claims;

namespace MySecondBrain.MVCApp.Controllers
{
    public class TagsController : Controller
    {
        private readonly MySecondBrain_LMContext _context;


        // GET: Tags
        public async Task<IActionResult> Index()
        {
            TagControllerService tagListControllerService = new TagControllerService();
            var TagsList = tagListControllerService.GetTagListViewModel();

            return View(TagsList);
        }

        public IActionResult Detail(int id)
        {
            var vm = TagControllerService.GetTagDetail(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            TagDetailViewModel vm = new TagDetailViewModel();

            return View();
        }

        public IActionResult Edit(int id)
        {
            var vm = TagControllerService.GetTagDetail(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(TagDetailViewModel tagDetailViewModel)
        {
            TagControllerService.EditTag(tagDetailViewModel.Tag);
            return View();
        }

        public IActionResult Delete(int id)
        {
            TagControllerService.DeleteTag(id);
            return RedirectToAction("Index");
        }

        // POST: Tags/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostCreate(TagDetailViewModel tagDetailViewModel)
        {
            int idTag = tagDetailViewModel.IDTag;

            TagControllerService.CreateTag(tagDetailViewModel.Tag, this.User.FindFirstValue(ClaimTypes.NameIdentifier), idTag);

            var tags = TagControllerService.GetTagsListOfUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("Index");
        }

        //// GET: Tags/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = await _context.Tags
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Idtag == id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tag);
        //}

        //// GET: Tags/Create
        //public IActionResult Create()
        //{
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
        //    return View();
        //}

        //// POST: Tags/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Idtag,Nom,UserId")] Tag tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(tag);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tag.UserId);
        //    return View(tag);
        //}

        //// GET: Tags/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = await _context.Tags.FindAsync(id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tag.UserId);
        //    return View(tag);
        //}

        //// POST: Tags/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Idtag,Nom,UserId")] Tag tag)
        //{
        //    if (id != tag.Idtag)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(tag);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TagExists(tag.Idtag))
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
        //    ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", tag.UserId);
        //    return View(tag);
        //}

        //// GET: Tags/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var tag = await _context.Tags
        //        .Include(t => t.User)
        //        .FirstOrDefaultAsync(m => m.Idtag == id);
        //    if (tag == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(tag);
        //}

        //// POST: Tags/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tag = await _context.Tags.FindAsync(id);
        //    _context.Tags.Remove(tag);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool TagExists(int id)
        //{
        //    return _context.Tags.Any(e => e.Idtag == id);
        //}
    }
}
