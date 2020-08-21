using System;
using System.Linq;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using StarShips.Models;

namespace StarShips.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IApiCaller _apiCaller;
        private readonly ICalculator _calculator;

        public HomeController(ILogger<HomeController> logger, IApiCaller apiCaller, ICalculator calculator)
        {
            _logger = logger;
            _apiCaller = apiCaller;
            _calculator = calculator;
        }

        /// <summary>
        /// Method of HomePage
        /// </summary>
        /// <returns>return View()</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Submit of Calculate button
        /// </summary>
        /// <param name="form"></param>
        /// <returns>View(listResultDTO)</returns>
        [HttpPost]
        public IActionResult Result(IFormCollection form)
        {
            GridResultViewModel listResultDTO = GetGridResult(form["txtDistance"]);
            if (listResultDTO == null)
                return RedirectToAction("Index", "Home");

            return View(listResultDTO);
        }

        /// <summary>
        /// API post rest the url
        /// </summary>
        /// <param name="MGLTView"></param>
        /// <returns>gridResultDTO</returns>
        public GridResultViewModel GetGridResult(string MGLTView)
        {
            var runner = new Runner(_calculator, _apiCaller);
            runner.Init();

            var listResultDTO = runner.Run(Convert.ToInt64(MGLTView));


            GridResultViewModel gridResultViewModel = new GridResultViewModel();
            if (!listResultDTO.Any())
                return gridResultViewModel;

            gridResultViewModel.MGLTView = MGLTView;
            gridResultViewModel.Count = runner.CountStarShips.ToString();
            gridResultViewModel.Next = "";
            gridResultViewModel.Previous = "";
            gridResultViewModel.ListResultDTO = listResultDTO;

            return gridResultViewModel;
        }
    }
}

