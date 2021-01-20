using CleanArchitecture.Application.Common.Interfaces;
using System;

namespace CleanArchitecture.Integration.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}