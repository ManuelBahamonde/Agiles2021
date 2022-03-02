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
        private static MainGame _mainGame;

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

        [HttpPost]
        public IActionResult TryLetter([FromBody] TryLetterRequest rq)
        {
            var letter = rq.Letter;
            try
            {
                var result = _mainGame.TryLetter(letter);

                return Json(BuildGameStatusInfoResponse(result));
            } catch (ArgumentException exc)
            {
                Response.StatusCode = 400;
                return Json(new { Error = exc.Message });
            } catch (InvalidOperationException exc)
            {
                Response.StatusCode = 400;
                return Json(new { Error = exc.Message });
            }
        }

        [HttpPost]
        public IActionResult TryWord([FromBody] TryWordRequest rq)
        {
            var word = rq.Word;

            try
            {
                var result = _mainGame.TryWord(word);

                return Json(BuildGameStatusInfoResponse(result));
            }
            catch (ArgumentException exc)
            {
                Response.StatusCode = 400;
                return Json(new { Error = exc.Message });
            }
            catch (InvalidOperationException exc)
            {
                Response.StatusCode = 400;
                return Json(new { Error = exc.Message });
            }
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

        #region Helpers
        private GameInfoModel BuildGameInfoModel() => new GameInfoModel
        {
            Name = _mainGame.Name,
            Result = _mainGame.Result,
            AttemptsLeft = _mainGame.AttemptsLeft,
            Difficulty = _mainGame.Difficulty.ToString(),
            IncorrectChars = _mainGame.IncorrectChars.ToList(),
        };

        private GameStatusInfoResponse BuildGameStatusInfoResponse(TryResponse result) => new()
        {
            IsMatch = result.Match,
            Result = _mainGame.Result,
            AttemptsLeft = _mainGame.AttemptsLeft,
            IncorrectChars = _mainGame.IncorrectChars.ToList(),
            GameOver = _mainGame.IsGameOver,
            Win = _mainGame.Win,
        }; 

    #endregion
}
}
