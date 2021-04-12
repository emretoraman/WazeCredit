using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics;
using WazeCredit.Models.ViewModels;
using WazeCredit.Services;
using WazeCredit.Utility.AppSettings;

namespace WazeCredit.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMarketForecaster _marketForecaster;
        private readonly WazeForecastSettings _wazeForecastSettings;
        private readonly StripeSettings _stripeSettings;
        private readonly TwilioSettings _twilioSettings;
        private readonly SendGridSettings _sendGridSettings;

        public HomeController(IMarketForecaster marketForecaster, 
            IOptions<WazeForecastSettings> wazeForecastSettings,
            IOptions<StripeSettings> stripeSettings,
            IOptions<TwilioSettings> twilioSettings,
            IOptions<SendGridSettings> sendGridSettings)
        {
            _marketForecaster = marketForecaster;
            _wazeForecastSettings = wazeForecastSettings.Value;
            _stripeSettings = stripeSettings.Value;
            _twilioSettings = twilioSettings.Value;
            _sendGridSettings = sendGridSettings.Value;
        }

        public IActionResult Index()
        {
            var marketCondition = _marketForecaster.GetMarketPrediction().MarketCondition.ToString();

            var homeVM = new HomeViewModel { MarketForecast = $"Market is {marketCondition}" };
            return View(homeVM);
        }

        public IActionResult AppSettings()
        {
            List<string> settings = new List<string>
            {
                $"WazeForecast.{nameof(WazeForecastSettings.ForecastTrackerEnabled)}: {_wazeForecastSettings.ForecastTrackerEnabled}",
                $"Stripe.{nameof(StripeSettings.SecretKey)}: {_stripeSettings.SecretKey}",
                $"Stripe.{nameof(StripeSettings.PublishableKey)}: {_stripeSettings.PublishableKey}",
                $"Twilio.{nameof(TwilioSettings.PhoneNumber)}: {_twilioSettings.PhoneNumber}",
                $"Twilio.{nameof(TwilioSettings.AuthToken)}: {_twilioSettings.AuthToken}",
                $"Twilio.{nameof(TwilioSettings.AccountSid)}: {_twilioSettings.AccountSid}",
                $"SendGrid.{nameof(SendGridSettings.SendGridKey)}: {_sendGridSettings.SendGridKey}"
            };

            return View(settings);
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
