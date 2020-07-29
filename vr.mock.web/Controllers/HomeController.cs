using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vr.mock.web.Models;
using vr.mock.web.Services;

namespace vr.mock.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITradingService _tradingService;

        public HomeController(ILogger<HomeController> logger,
            ITradingService tradingService)
        {
            _logger = logger;
            _tradingService = tradingService;
        }

        public async Task<IActionResult> Index()
        {
            //var viewModel = new TradeViewModel()
            //{
            //    Trades = new List<Trade>
            //    {
            //        new Trade { Ticker = "TESLA", BuyPrice = 100m, SellPrice = 105m},
            //        new Trade { Ticker = "GOOG", BuyPrice = 100m, SellPrice = 105m}
            //    }
            //};
            var viewModel = await this._tradingService.GetTradeViewModel();
            return View(viewModel);
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
