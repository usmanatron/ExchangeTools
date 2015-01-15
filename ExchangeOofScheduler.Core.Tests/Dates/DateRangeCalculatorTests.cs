using System;
using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  class DateRangeCalculatorTests
  {
    private DateRangeCalculator dateRangeCalculator;
    
    [SetUp]
    public void Setup()
    {
      // Wednesday 14th January 2015 @3pm
      var clock = new TestClock(new DateTime(2015, 1, 14, 12, 0, 0));
      dateRangeCalculator = new DateRangeCalculator(clock);
    }

    [Test]
    public void DateBeforeWednesday_ChoosesNextWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Monday, DayOfWeek.Monday);

      // Sunday 6pm - Monday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 18, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 19, 18, 0, 0), range.End);
    }

    [Test]
    public void DateAfterWednesday_ChoosesSameWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Friday, DayOfWeek.Friday);

      // Thursday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 15, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateIsWednesday_ChoosesSameWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Wednesday, DayOfWeek.Wednesday);

      // Thursday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 13, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 14, 18, 0, 0), range.End);
    }
    
    [Test]
    public void DateRangeAfterWednesday_SpanningMultipleDays_GivesExpectedRangeInSameWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Thursday, DayOfWeek.Friday);

      // Wednesday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 14, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeBeforeWednesday_SpanningMultipleDays_GivesExpectedRangeNextWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Monday, DayOfWeek.Tuesday);

      // Sunday 6pm - Tuesday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 18, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 20, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeStartingWednesday_SelectsThisWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Wednesday, DayOfWeek.Friday);

      // Tuesday 6pm (yesterday!) - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 13, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeEndingOnWednesday_SelectsNextWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Monday, DayOfWeek.Wednesday);

      // Sunday 6pm (next week) - Wednesday 6pm (next week)
      Assert.AreEqual(new DateTime(2015, 1, 18, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 21, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeSpanningBetweenWednesday_SelectsThisWeek()
    {
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(DayOfWeek.Tuesday, DayOfWeek.Thursday);

      // Monday (in the past) 6pm - Thursday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 12, 18, 0, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 15, 18, 0, 0), range.End);
    }
  }
}