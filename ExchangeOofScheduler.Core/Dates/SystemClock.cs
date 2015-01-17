using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public class SystemClock : IClock
  {
    public DateTime Now
    {
      get { return DateTime.Now; }
    }
  }
}