﻿using System;

namespace WazeCredit.Services.Lifetime
{
    public class SingletonService
    {
        private readonly Guid _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid();
        }

        public string GetGuid() => _guid.ToString();
    }
}
