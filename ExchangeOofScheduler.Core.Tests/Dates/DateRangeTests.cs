using System;
using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  class DateRangeTests
  {
    private static readonly DateTime wayPastDate = DateTime.Now.AddDays(-7);
    private static readonly DateTime pastDate = DateTime.Now.AddDays(-1);
    private static readonly DateTime futureDate = DateTime.Now.AddDays(1);
    private static readonly DateTime wayFutureDate = DateTime.Now.AddDays(7);

    [Test]
    [TestCaseSource("nonIntersectingDateRanges")]
    public void RangeWhichDoesNotIntersectNow_IsntHappeningNow(Tuple<DateTime, DateTime> range)
    {
      var dateRange = new DateRange(range.Item1, range.Item2);
      Assert.False(dateRange.HappeningNow);
    }

    [Test]
    [TestCaseSource("intersectingDateRanges")]
    public void RangeWhichDoesIntersectNow_HappeningNow(Tuple<DateTime, DateTime> range)
    {
      var dateRange = new DateRange(range.Item1, range.Item2);
      Assert.True(dateRange.HappeningNow);
    }

    [Test]
    [TestCaseSource("exceptioningDateRanges")]
    public void RangeWithDatesInDescendingOrder_Throws(Tuple<DateTime, DateTime> range)
    {
      Assert.Throws<ArgumentException>(() => new DateRange(range.Item1, range.Item2));
    }

    private readonly object[] nonIntersectingDateRanges =
    {
      new Tuple<DateTime, DateTime>(wayPastDate, pastDate),
      new Tuple<DateTime, DateTime>(futureDate, wayFutureDate)
    };

    private readonly object[] intersectingDateRanges =
    {
      new Tuple<DateTime, DateTime>(wayPastDate, futureDate),
      new Tuple<DateTime, DateTime>(wayPastDate, wayFutureDate),
      new Tuple<DateTime, DateTime>(pastDate, futureDate),
      new Tuple<DateTime, DateTime>(pastDate, wayFutureDate)
    };

    // These date ranges are the wrong way around
    private readonly object[] exceptioningDateRanges=
    {
      new Tuple<DateTime, DateTime>(wayFutureDate, futureDate),
      new Tuple<DateTime, DateTime>(wayFutureDate, pastDate),
      new Tuple<DateTime, DateTime>(wayFutureDate, wayPastDate),
      new Tuple<DateTime, DateTime>(futureDate, pastDate),
      new Tuple<DateTime, DateTime>(futureDate, wayPastDate),
      new Tuple<DateTime, DateTime>(pastDate, wayPastDate)
    };
  }
}
