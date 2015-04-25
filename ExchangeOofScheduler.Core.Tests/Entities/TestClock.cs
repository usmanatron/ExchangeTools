using ExchangeOofScheduler.Core.Entities;
using System;

namespace ExchangeOofScheduler.Core.Tests.Entities
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