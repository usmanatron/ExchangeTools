using System;
using ExchangeOofScheduler.Core.Dates;

namespace ExchangeOofScheduler.Core
{
  public class OofScheduleBuilder
  {
    private readonly DayOfWeekReader dayOfWeekReader;

    public OofScheduleBuilder(DayOfWeekReader dayOfWeekReader)
    {
      this.dayOfWeekReader = dayOfWeekReader;
    }

    public OofSchedule Build(string startDay, string startTime, string endDay, string endTime)
    {
      return new OofSchedule(dayOfWeekReader.Read(startDay),
                             DateTime.Parse(startTime).TimeOfDay,
                             dayOfWeekReader.Read(endDay),
                             DateTime.Parse(endTime).TimeOfDay);
    }
  }
}
