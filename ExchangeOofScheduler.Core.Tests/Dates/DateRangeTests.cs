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
    [TestCaseSource("exceptioningDateRanges")]
    public void RangeWithDatesInDescendingOrder_Throws(Tuple<DateTime, DateTime> range)
    {
      Assert.Throws<ArgumentException>(() => new DateRange(range.Item1, range.Item2));
    }
  }
}
