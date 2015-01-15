using System;
using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  class DateRangeTests
  {
    [Test]
    public void RangeWithDatesInDescendingOrder_Throws()
    {
      Assert.Throws<ArgumentException>(() => new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1)));
    }
  }
}