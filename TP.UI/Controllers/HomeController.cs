using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tp.Models;
using TP.Game;
using TP.UI.Models;

namespace TP.UI.Controllers
{
    public class HomeController : Controller
    {
        private MainGame _mainGame;

        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StartGame(string playerName, int difficultyId)
        {
            Difficulty difficulty;
            if ((int)Difficulty.Easy == difficultyId)
                difficulty = Difficulty.Easy;
            else if ((int)Difficulty.Medium == difficultyId)
                difficulty = Difficulty.Medium;
            else
                difficulty = Difficulty.Hard;

            // TODO: Implement the rest of the requirements
            try
            {
                _mainGame = new MainGame(playerName, difficulty);
            } catch(ArgumentException exc)
            {
                ModelState.AddModelError("Argument", exc.Message);
                return View("Index");
            }

            return View("MainGame", BuildGameInfoModel());
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

        private GameInfoModel BuildGameInfoModel() => new GameInfoModel
        {
            Name = _mainGame.Name,
            Result = _mainGame.Result,
            AttemptsLeft = _mainGame.AttemptsLeft,
            Difficulty = _mainGame.Difficulty.ToString(),
            IncorrectChars = _mainGame.IncorrectChars.ToList(),
        };
    }
}
