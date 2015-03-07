using System;

namespace Models
{
    public interface IUnlimitedPass : IPass
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
    }
}