using System;
using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class CreditValidationChecker : IValidationChecker
    {
        public string ErrorMessage => "Age/Salary/Credit validation failed";

        public bool ValidatorLogic(CreditApplication creditApplication)
        {
            if (DateTime.Now.AddYears(-18) < creditApplication.DOB)
            {
                return false;
            }
            if (creditApplication.Salary < 10000)
            {
                return false;
            }
            return true;
        }
    }
}
