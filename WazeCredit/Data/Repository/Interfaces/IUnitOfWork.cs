using System;

namespace WazeCredit.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICreditApplicationRepository CreditApplicationRepository { get; }
        void Save();
    }
}
