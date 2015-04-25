using ExchangeOofScheduler.Core.Entities;

namespace ExchangeOofScheduler.Core.Dates
{
  public interface IDateRangeCalculator
  {
    DateRange CalculateNextDateRangeForOof(OofSchedule schedule);
  }
}