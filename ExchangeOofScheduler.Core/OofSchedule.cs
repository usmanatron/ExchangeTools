using System;

namespace ExchangeOofScheduler.Core
{
  /// <summary>
  /// Defines an OOF Schedule (essentially a start day and time and end day and time)
  /// </summary>
  public class OofSchedule
  {
    public DayOfWeek startDay { get; private set; }

    public TimeSpan startTime { get; private set; }

    public DayOfWeek endDay { get; private set; }

    public TimeSpan endTime { get; private set; }

    public OofSchedule(DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
    {
      this.startDay = startDay;
      this.startTime = startTime;
      this.endDay = endDay;
      this.endTime = endTime;
    }
  }
}