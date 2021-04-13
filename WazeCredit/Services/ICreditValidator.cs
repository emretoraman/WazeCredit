using System.Collections.Generic;
using WazeCredit.Models;

namespace WazeCredit.Services
{
    public interface ICreditValidator
    {
        (bool, IEnumerable<string>) PassAllValidations(CreditApplication creditApplication);
    }
}
