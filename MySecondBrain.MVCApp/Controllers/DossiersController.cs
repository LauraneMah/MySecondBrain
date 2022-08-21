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
    public class DossiersController : Controller
    {
        private readonly MySecondBrain_LMContext _context;

        // GET: Dossiers
        public async Task<IActionResult> Index()
        {
            DossierControllerService dossierListControllerService = new DossierControllerService();
            var DossiersList = dossierListControllerService.GetDossierListViewModel();

            return View(DossiersList);
        }

        public IActionResult Detail(int id)
        {
            var vm = DossierControllerService.GetDossierDetail(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            DossierDetailViewModel vm = DossierControllerService.GetDossierDetail();

            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            var vm = DossierControllerService.GetDossierDetail(id);
            if (vm == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DossierDetailViewModel dossierDetailViewModel)
        {
            DossierControllerService.EditDossier(dossierDetailViewModel.Dossier);
            return View();
        }

        public IActionResult Delete(int id)
        {
            DossierControllerService.DeleteDossier(id);
            return RedirectToAction("Index");
        }

        // POST: Dossiers/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostCreate(DossierDetailViewModel dossierDetailViewModel)
        {
            int idDossierParent = dossierDetailViewModel.IDDossierParent;

            DossierControllerService.CreateDossier(dossierDetailViewModel.Dossier, this.User.FindFirstValue(ClaimTypes.NameIdentifier), idDossierParent);

            var dossiers = DossierControllerService.GetDossiersListOfUser(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("Index");
        }
    }
}
