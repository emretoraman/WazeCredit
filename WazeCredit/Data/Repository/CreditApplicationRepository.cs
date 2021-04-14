using WazeCredit.Data.Repository.Interfaces;
using WazeCredit.Models;

namespace WazeCredit.Data.Repository
{
    public class CreditApplicationRepository : Repository<CreditApplication>, ICreditApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CreditApplicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(CreditApplication creditApplication)
        {
            _dbContext.CreditApplications.Update(creditApplication);
        }
    }
}
