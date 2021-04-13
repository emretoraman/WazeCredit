using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class AddressValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "Address validation failed";

        public bool ValidatorLogic(CreditApplication creditApplication)
        {
            if (creditApplication.PostalCode <= 0 || creditApplication.PostalCode > 99999)
            {
                return false;
            }
            return true;
        }
    }
}
