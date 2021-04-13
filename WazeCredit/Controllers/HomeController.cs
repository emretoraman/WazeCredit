using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
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
        
        [BindProperty]
        public CreditApplication CreditApplicationModel { get; set; }

        public HomeController(IMarketForecaster marketForecaster, ICreditValidator creditValidator)
        {
            _marketForecaster = marketForecaster;
            _creditValidator = creditValidator;
        }

        public IActionResult Index()
        {
            var marketCondition = _marketForecaster.GetMarketPrediction().MarketCondition.ToString();

            var homeVM = new HomeViewModel { MarketForecast = $"Market is {marketCondition}" };
            return View(homeVM);
        }

        public IActionResult AppSettings(
            [FromServices] IOptions<WazeForecastSettings> wazeForecastSettings,
            [FromServices] IOptions<StripeSettings> stripeSettings,
            [FromServices] IOptions<TwilioSettings> twilioSettings,
            [FromServices] IOptions<SendGridSettings> sendGridSettings)
        {
            List<string> settings = new List<string>
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

        public IActionResult CreditApplication()
        {
            CreditApplicationModel = new CreditApplication();
            return View(CreditApplicationModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [ActionName("CreditApplication")]
        public IActionResult CreditApplicationPOST()
        {
            if (ModelState.IsValid)
            {
                var (validationPassed, errorMessages) = _creditValidator.PassAllValidations(CreditApplicationModel);
                var creditResult = new CreditResult
                {
                    ErrorList = errorMessages,
                    CreditID = 0,
                    Success = validationPassed
                };

                if (validationPassed)
                {
                    //todo: Add to DB
                }
                return RedirectToAction(nameof(CreditResult), creditResult);
            }
            return View(CreditApplicationModel);
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
