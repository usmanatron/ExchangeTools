using ExchangeOofScheduler.Core.Entities;

namespace ExchangeOofScheduler.Core.Builders
{
  public interface IOofScheduleBuilder
  {
    OofSchedule Build(string startDay, string startTime, string endDay, string endTime);
  }
}