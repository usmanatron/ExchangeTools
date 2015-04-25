using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Entities;
using System;

namespace ExchangeOofScheduler.Core.Builders
{
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