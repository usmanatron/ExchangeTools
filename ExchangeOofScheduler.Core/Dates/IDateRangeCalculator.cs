using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public interface IDateRangeCalculator
  {
    DateRange CalculateNextDateRangeForOof(DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime);
  }
}
