using System;

namespace ExchangeTools.Core.Entities
{
  public class SystemClock : IClock
  {
    public DateTime Now
    {
      get { return DateTime.Now; }
    }
  }
}