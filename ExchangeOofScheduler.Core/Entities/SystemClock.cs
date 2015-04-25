using System;

namespace ExchangeOofScheduler.Core.Entities
{
  public class SystemClock : IClock
  {
    public DateTime Now
    {
      get { return DateTime.Now; }
    }
  }
}