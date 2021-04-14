using WazeCredit.Models;

namespace WazeCredit.Data.Repository.Interfaces
{
    public interface ICreditApplicationRepository : IRepository<CreditApplication>
    {
        void Update(CreditApplication creditApplication);
    }
}
