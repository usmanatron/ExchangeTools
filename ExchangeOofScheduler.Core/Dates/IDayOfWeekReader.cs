using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public interface IDayOfWeekReader
  {
    DayOfWeek Read(string input);
  }
}