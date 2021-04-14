using System;
using WazeCredit.Data.Repository.Interfaces;

namespace WazeCredit.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            CreditApplicationRepository = new CreditApplicationRepository(_dbContext);
        }

        public ICreditApplicationRepository CreditApplicationRepository { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
