using ExchangeTools.Core;
using System;

namespace ExchangeTools.TestHelpers
{
  public class TestClock : IClock
  {
    private readonly DateTime dateTime;

    public TestClock(DateTime dateTime)
    {
      this.dateTime = dateTime;
    }

    public DateTime Now
    {
      get { return dateTime; }
    }
  }
}