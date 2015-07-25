using System;

namespace ExchangeTools.Core.Dates
{
  public interface IDayOfWeekReader
  {
    DayOfWeek Read(string input);
  }
}