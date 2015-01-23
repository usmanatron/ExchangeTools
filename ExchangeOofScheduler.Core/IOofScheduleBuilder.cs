namespace ExchangeOofScheduler.Core
{
  public interface IOofScheduleBuilder
  {
    OofSchedule Build(string startDay, string startTime, string endDay, string endTime);
  }
}