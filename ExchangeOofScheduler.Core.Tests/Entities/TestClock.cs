using ExchangeTools.Core.Entities;
using System;

namespace ExchangeTools.Core.Tests.Entities
{
  internal class TestClock : IClock
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