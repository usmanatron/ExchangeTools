using System;
using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  class DateRangeTests
  {
    private DateTime wayPastDate;
    private DateTime pastDate;
    private DateTime now;
    private DateTime futureDate;
    private DateTime wayFutureDate;

    private object[] nonIntersectingDateRanges;
    private object[] intersectingDateRanges;
    // These date ranges are the wrong way around
    private object[] exceptioningDateRanges;

    [SetUp]
    public void Setup()
    {
      wayPastDate = DateTime.Now.AddDays(-7);
      pastDate = DateTime.Now.AddDays(-1);
      now = DateTime.Now;
      futureDate = DateTime.Now.AddDays(1);
      wayFutureDate = DateTime.Now.AddDays(7);

      nonIntersectingDateRanges = new object[]
                                  {
                                    new Tuple<DateTime, DateTime>(wayPastDate, pastDate),
                                    new Tuple<DateTime, DateTime>(futureDate, wayFutureDate)
                                  };
      intersectingDateRanges = new object[]
                               {
                                 new Tuple<DateTime, DateTime>(wayPastDate, futureDate),
                                 new Tuple<DateTime, DateTime>(wayPastDate, wayFutureDate),
                                 new Tuple<DateTime, DateTime>(pastDate, futureDate),
                                 new Tuple<DateTime, DateTime>(pastDate, wayFutureDate)
                               };
      exceptioningDateRanges = new object[]
                               {
                                 new Tuple<DateTime, DateTime>(wayFutureDate, futureDate),
                                 new Tuple<DateTime, DateTime>(wayFutureDate, pastDate),
                                 new Tuple<DateTime, DateTime>(wayFutureDate, wayPastDate),
                                 new Tuple<DateTime, DateTime>(futureDate, pastDate),
                                 new Tuple<DateTime, DateTime>(futureDate, wayPastDate),
                                 new Tuple<DateTime, DateTime>(pastDate, wayPastDate)
                               };
    }

    [Test]
    [TestCaseSource("nonIntersectingDateRanges")]
    public void RangeWhichDoesNotIntersectNow_IsntHappeningNow(Tuple<DateTime, DateTime> range)
    {
      var dateRange = new DateRange(range.Item1, range.Item2, now);
      Assert.False(dateRange.HappeningNow);
    }

    [Test]
    [TestCaseSource("intersectingDateRanges")]
    public void RangeWhichDoesIntersectNow_HappeningNow(Tuple<DateTime, DateTime> range)
    {
      var dateRange = new DateRange(range.Item1, range.Item2, now);
      Assert.True(dateRange.HappeningNow);
    }

    [Test]
    [TestCaseSource("exceptioningDateRanges")]
    public void RangeWithDatesInDescendingOrder_Throws(Tuple<DateTime, DateTime> range)
    {
      Assert.Throws<ArgumentException>(() => new DateRange(range.Item1, range.Item2, now));
    }
  }
}
