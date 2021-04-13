using WazeCredit.Models;

namespace WazeCredit.Services
{
    public interface IValidationChecker
    {
        bool ValidatorLogic(CreditApplication creditApplication);
        string ErrorMessage { get; }
    }
}
