using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;
using System;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  internal class DateRangeTests
  {
    [Test]
    public void RangeWithDatesInDescendingOrder_Throws()
    {
      Assert.Throws<ArgumentException>(() => new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(-1)));
    }
  }
}