using System;

namespace ExchangeTools.OofScheduler.Entities
{
  /// <summary>
  /// Defines an OOF Schedule (essentially a start day and time and end day and time)
  /// </summary>
  public class OofSchedule
  {
    public DayOfWeek StartDay { get; private set; }

    public TimeSpan StartTime { get; private set; }

    public DayOfWeek EndDay { get; private set; }

    public TimeSpan EndTime { get; private set; }

    public OofSchedule(DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
    {
      StartDay = startDay;
      StartTime = startTime;
      EndDay = endDay;
      EndTime = endTime;
    }
  }
}