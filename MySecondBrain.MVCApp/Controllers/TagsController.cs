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
    }
}
