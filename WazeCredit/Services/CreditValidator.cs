using System.Collections.Generic;
using System.Linq;
using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class CreditValidator : ICreditValidator
    {
        private readonly IEnumerable<IValidationChecker> _validations;

        public CreditValidator(IEnumerable<IValidationChecker> validations)
        {
            _validations = validations;
        }

        public (bool, IEnumerable<string>) PassAllValidations(CreditApplication creditApplication)
        {
            var errorMessages = new List<string>();
            foreach (var validation in _validations)
            {
                if (!validation.ValidatorLogic(creditApplication))
                {
                    errorMessages.Add(validation.ErrorMessage);
                }
            }
            return (!errorMessages.Any(), errorMessages);
        }
    }
}
