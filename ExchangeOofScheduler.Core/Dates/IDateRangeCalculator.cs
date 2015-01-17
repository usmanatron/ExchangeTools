namespace ExchangeOofScheduler.Core.Dates
{
  public interface IDateRangeCalculator
  {
    DateRange CalculateNextDateRangeForOof(OofSchedule schedule);
  }
}