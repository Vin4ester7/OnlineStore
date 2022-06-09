using Microsoft.AspNetCore.Mvc;
using OnlineStore.Interfaces;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class GamesController : Controller
    {
        static string TableName { get; set; }
        static int IdNumber { get; set; }
        private readonly IGamesRepository _gamesRepository;

        public GamesController( IGamesRepository repository)
        {
            _gamesRepository = repository;
        }

        [Route("Games/Zombies/{id?}")]
        public IActionResult Zombies(int? id)
        {
            TableName = "Zombies";
            return Game(id, "Zombies");
        }

        [Route("Games/Adventure/{id?}")]
        public IActionResult Adventure(int? id)
        {
            TableName = "Adventure";
            return Game(id, "Adventure");
        }

        [Route("Games/Survival/{id?}")]
        public IActionResult Survival(int? id)
        {
            TableName = "Survival";
            return Game(id, "Survival");
        }

        [Route("Games/Strategy/{id?}")]
        public IActionResult Strategy(int? id)
        {
            TableName = "Strategy";
            return Game(id, "Strategy");
        }

        [Route("Games/Shooters/{id?}")]
        public IActionResult Shooters(int? id)
        {
            TableName = "Shooters";
            return Game(id, "Shooters");
        }

        [Route("Games/Horror/{id?}")]
        public IActionResult Horror(int? id)
        {
            TableName = "Horror";
            return Game(id, "Horror");
        }

        [Route("Games/Fighting/{id?}")]
        public IActionResult Fighting(int? id)
        {
            TableName = "Fighting";
            return Game(id, "Fighting");
        }

        [Route("Games/Russian/{id?}")]
        public IActionResult Russian(int? id)
        {
            TableName = "Russian";
            return Game(id, "Russian");
        }

        public IActionResult Game(int? id, string tableName)
        {
            if (id == null)
            {
                var games = _gamesRepository.GetGamesByCategory(tableName);
                return View("GamesByCategory", games);
            }

            var product = _gamesRepository.GetProduct((int)id, tableName);
            if (product == null)
            {
                return View("Error");
            }

            return View("Index", product);
        }

        [Route("Games")]
        public IActionResult AllGames()
        {
            var allGames = _gamesRepository.GetAllGames();

            if (allGames == null)
            {
                return View("Error");
            }

            return View(allGames);
        }

        public async Task<IActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Создание товара" : "Редактирование товара";
            ViewBag.IsEdit = Id == null ? false : true;
            Game game;
            if (Id == null)
            {
                game = new Game();
                game.Id = _gamesRepository.GetFreeId();
                IdNumber = game.Id;
            }
            else
            {
                game = _gamesRepository.GetGame((int)Id);
                IdNumber = (int)Id;
                if (game == null)
                {
                    return NotFound();
                }
            }
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(Game model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var check = _gamesRepository.GetProduct(IdNumber, TableName);
                    if (check == null)
                    {
                        model.Id = IdNumber;
                        _gamesRepository.CreateGame(model, TableName);
                    }
                    else
                    {
                        _gamesRepository.UpdateCategory(model);
                    }
                    return RedirectToAction("Index", "Home"); ;
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return BadRequest("Модель неверна.");
            }
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            _gamesRepository.Delete((int)Id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Payment(int? id, string tableName)
        {
            if (id != null && tableName != null)
            {
                var game = _gamesRepository.GetProduct((int)id, tableName);
                if (game != null)
                {
                    return View(game);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}
