using ExchangeTools.OofScheduler.Dates;
using ExchangeTools.OofScheduler.Entities;
using System;

namespace ExchangeTools.OofScheduler.Builders
{
  public interface IOofScheduleBuilder
  {
    OofSchedule Build(string startDay, string startTime, string endDay, string endTime);
  }

  public class OofScheduleBuilder : IOofScheduleBuilder
  {
    private readonly IDayOfWeekReader dayOfWeekReader;

    public OofScheduleBuilder(IDayOfWeekReader dayOfWeekReader)
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