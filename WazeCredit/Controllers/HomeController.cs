using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WazeCredit.Data;
using WazeCredit.Models;
using WazeCredit.Models.ViewModels;
using WazeCredit.Services;
using WazeCredit.Utility.AppSettings;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarketForecaster _marketForecaster;
        private readonly ICreditValidator _creditValidator;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger _logger;
        
        [BindProperty]
        public CreditApplication CreditApplication { get; set; }

        public HomeController(IMarketForecaster marketForecaster, 
            ICreditValidator creditValidator, 
            ApplicationDbContext dbContext,
            ILogger<HomeController> logger)
        {
            _marketForecaster = marketForecaster;
            _creditValidator = creditValidator;
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("HomeController Index action called");

            var marketCondition = _marketForecaster.GetMarketPrediction().MarketCondition.ToString();
            var homeVM = new HomeViewModel { MarketForecast = $"Market is {marketCondition}" };

            _logger.LogInformation("HomeController Index action ended");
            return View(homeVM);
        }

        public IActionResult AppSettings(
            [FromServices] IOptions<WazeForecastSettings> wazeForecastSettings,
            [FromServices] IOptions<StripeSettings> stripeSettings,
            [FromServices] IOptions<TwilioSettings> twilioSettings,
            [FromServices] IOptions<SendGridSettings> sendGridSettings)
        {
            var settings = new List<string>
            {
                $"WazeForecast.{nameof(WazeForecastSettings.ForecastTrackerEnabled)}: {wazeForecastSettings.Value.ForecastTrackerEnabled}",
                $"Stripe.{nameof(StripeSettings.SecretKey)}: {stripeSettings.Value.SecretKey}",
                $"Stripe.{nameof(StripeSettings.PublishableKey)}: {stripeSettings.Value.PublishableKey}",
                $"Twilio.{nameof(TwilioSettings.PhoneNumber)}: {twilioSettings.Value.PhoneNumber}",
                $"Twilio.{nameof(TwilioSettings.AuthToken)}: {twilioSettings.Value.AuthToken}",
                $"Twilio.{nameof(TwilioSettings.AccountSid)}: {twilioSettings.Value.AccountSid}",
                $"SendGrid.{nameof(SendGridSettings.SendGridKey)}: {sendGridSettings.Value.SendGridKey}"
            };

            return View(settings);
        }

        [ActionName("CreditApplication")]
        public IActionResult CreditApplicationGET()
        {
            CreditApplication = new CreditApplication();
            return View(CreditApplication);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("CreditApplication")]
        public IActionResult CreditApplicationPOST([FromServices] Func<CreditApprovedEnum, ICreditApproved> creditService)
        {
            if (ModelState.IsValid)
            {
                var (validationPassed, errorMessages) = _creditValidator.PassAllValidations(CreditApplication);
                var creditResult = new CreditResult
                {
                    ErrorList = errorMessages,
                    CreditID = 0,
                    Success = validationPassed
                };

                if (validationPassed)
                {
                    var creditApprovedEnum = CreditApplication.Salary > 50000 ? CreditApprovedEnum.High : CreditApprovedEnum.Low;
                    CreditApplication.CreditApproved = creditService(creditApprovedEnum).GetCreditApproved(CreditApplication);

                    _dbContext.Add(CreditApplication);
                    _dbContext.SaveChanges();
                    creditResult.CreditID = CreditApplication.Id;
                    creditResult.CreditApproved = CreditApplication.CreditApproved;
                }
                return RedirectToAction(nameof(CreditResult), creditResult);
            }
            return View(CreditApplication);
        }

        public IActionResult CreditResult(CreditResult creditResult)
        {
            return View(creditResult);
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
