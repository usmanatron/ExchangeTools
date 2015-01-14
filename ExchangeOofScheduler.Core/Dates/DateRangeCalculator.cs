using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public class DateRangeCalculator
  {
    /// <remarks>
    /// Once we have the correct dates, we update the DateTimes as follows:
    /// * Make the startDate one day before and start at 6pm (so the Oof is set the evening before)
    /// * Make the endDate end at 6pm (so it is always disabled at the end of the working day)
    /// </remarks>
    public DateRange CalculateNextDateRangeForOof(DayOfWeek startDay, DayOfWeek endDay)
    {
      var today = DateTime.Today;

      var oofStartDate = GetNextDateForDayOfWeek(today, startDay);
      var oofEndDate = GetNextDateForDayOfWeek(today, endDay);
      
      // Update the times of these dates as follows:
      oofStartDate = oofStartDate.AddDays(-1);
      oofStartDate = SetTime(oofStartDate);
      oofEndDate = SetTime(oofEndDate);

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

    private DateTime SetTime(DateTime dateTime)
    {
      var newTime = new TimeSpan(18, 00, 00);
      return dateTime.Date + newTime;
    }
  }
}