using ExchangeTools.OofScheduler.Entities;
using NUnit.Framework;
using System;

namespace ExchangeTools.Core.Tests.Entities
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