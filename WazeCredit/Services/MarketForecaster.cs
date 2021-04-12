using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class MarketForecaster
    {
        public MarketResult GetMarketPrediction()
        {
            return new MarketResult { MarketCondition = MarketCondition.StableUp };
        }
    }
}
