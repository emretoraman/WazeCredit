using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WazeCredit.Models.ViewModels;
using WazeCredit.Services;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var marketForecaster = new MarketForecaster();
            //var marketForecaster = new MarketForecasterV2();
            var marketCondition = marketForecaster.GetMarketPrediction().MarketCondition.ToString();

            var homeVM = new HomeViewModel { MarketForecast = $"Market is {marketCondition}" };
            return View(homeVM);
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
