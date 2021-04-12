using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class MarketForecasterV2
    {
        public MarketResult GetMarketPrediction()
        {
            return new MarketResult { MarketCondition = MarketCondition.Volatile };
        }
    }
}
