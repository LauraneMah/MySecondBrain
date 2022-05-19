using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySecondBrain.Application.Services;
using MySecondBrain.MVCApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MySecondBrain.MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NoteListControllerService _noteListControllerService;

        public HomeController(ILogger<HomeController> logger, NoteListControllerService noteListControllerService)
        {
            _logger = logger;
            _noteListControllerService = noteListControllerService;
        }

        public IActionResult Index()
        {
            var NotesList = _noteListControllerService.GetNotesListViewModel();

            return View(NotesList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
