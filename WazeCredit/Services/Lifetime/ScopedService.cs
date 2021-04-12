using System;

namespace WazeCredit.Services.Lifetime
{
    public class ScopedService
    {
        private readonly Guid _guid;

        public ScopedService()
        {
            _guid = Guid.NewGuid();
        }

        public string GetGuid() => _guid.ToString();
    }
}
