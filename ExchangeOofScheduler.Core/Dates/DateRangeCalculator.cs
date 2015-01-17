using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public class DateRangeCalculator : IDateRangeCalculator
  {
    private readonly IClock clock;

    public DateRangeCalculator(IClock clock)
    {
      this.clock = clock;
    }

    /// <remarks>
    /// Once we have the correct dates, we update the DateTimes as follows:
    /// * Make the startDate one day before and start at 6pm (so the Oof is set the evening before)
    /// * Make the endDate end at 6pm (so it is always disabled at the end of the working day)
    /// </remarks>
    public DateRange CalculateNextDateRangeForOof(DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
    {
      var datetimeNow = clock.Now;
      var oofStartDate = GetNextDateForDayOfWeek(datetimeNow, startDay);
      var oofEndDate = GetNextDateForDayOfWeek(datetimeNow, endDay);

      if (oofEndDate < oofStartDate)
      {
        if (datetimeNow.DayOfWeek == oofEndDate.DayOfWeek)
        {
          oofEndDate = oofEndDate.AddDays(7);
        }
        else
        {
          oofStartDate = oofStartDate.AddDays(-7);
        }
      }

      oofStartDate = oofStartDate.AddDays(-1);
      oofStartDate = oofStartDate.Date + startTime;
      oofEndDate = oofEndDate.Date + endTime;

      return new DateRange(oofStartDate, oofEndDate);
    }

    /// <remarks>
    /// Ideas taken from http://stackoverflow.com/questions/6346119/datetime-get-next-tuesday
    /// </remarks>
    private DateTime GetNextDateForDayOfWeek(DateTime now, DayOfWeek dayOfWeek)
    {
      // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
      int daysUntilDate = ((int)dayOfWeek - (int)now.DayOfWeek + 7) % 7;
      return now.AddDays(daysUntilDate);
    }
  }
}