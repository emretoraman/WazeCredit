using WazeCredit.Models;

namespace WazeCredit.Services
{
    public class CreditApprovedHigh : ICreditApproved
    {
        public double GetCreditApproved(CreditApplication creditApplication)
        {
            return creditApplication.Salary * .3;
        }
    }
}
