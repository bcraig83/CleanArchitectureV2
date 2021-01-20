using System;

namespace CleanArchitecture.Application.Common.Interfaces
{
    internal interface IDateTimeService
    {
        DateTime Now { get; }
    }
}