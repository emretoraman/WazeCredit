using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class CreditApprovedLow : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            return creditApplication.Salary * .5;
        }
    }
}
