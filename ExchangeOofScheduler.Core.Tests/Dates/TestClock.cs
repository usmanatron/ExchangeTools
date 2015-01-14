using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeOofScheduler.Core.Dates;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  class TestClock : IClock
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
