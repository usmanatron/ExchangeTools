using ExchangeTools.Core.Entities;

namespace ExchangeTools.Core.Builders
{
  public interface IOofScheduleBuilder
  {
    OofSchedule Build(string startDay, string startTime, string endDay, string endTime);
  }
}