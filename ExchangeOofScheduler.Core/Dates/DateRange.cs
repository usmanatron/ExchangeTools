using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public class DateRange
  {
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public DateRange(DateTime start, DateTime end)
    {
      if (start > end)
      {
        var message = string.Format("The Start date given ({0}) occurs after the end date! ({1})", start, end);
        throw new ArgumentException(message);
      }

      Start = start;
      End = end;
    }
  }
}
