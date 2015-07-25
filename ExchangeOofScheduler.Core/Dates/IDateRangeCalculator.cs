using ExchangeTools.Core.Entities;

namespace ExchangeTools.Core.Dates
{
  public interface IDateRangeCalculator
  {
    DateRange CalculateNextDateRangeForOof(OofSchedule schedule);
  }
}