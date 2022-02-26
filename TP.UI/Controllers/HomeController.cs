using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tp.Models;
using TP.UI.Models;

namespace TP.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var difficulties = new List<SelectListItem>
            {
                new SelectListItem { Value = Difficulty.Easy.ToString("D"), Text = "Facil"},
                new SelectListItem { Value = Difficulty.Medium.ToString("D"), Text = "Intermedio"},
                new SelectListItem { Value = Difficulty.Hard.ToString("D"), Text = "Dificil"},
            };

            return View(difficulties);
        }

        public IActionResult StartGame(int difficultyId)
        {
            Difficulty difficulty;
            if ((int)Difficulty.Easy == difficultyId)
                difficulty = Difficulty.Easy;
            else if ((int)Difficulty.Medium == difficultyId)
                difficulty = Difficulty.Medium;
            else
                difficulty = Difficulty.Hard;

            // TODO: create Game instance and implement the rest of the requirements

            return View("MainGame", difficulty.ToString());
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
