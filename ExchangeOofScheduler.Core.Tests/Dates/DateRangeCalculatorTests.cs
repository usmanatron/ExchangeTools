using System;
using ExchangeOofScheduler.Core.Dates;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Dates
{
  [TestFixture]
  class DateRangeCalculatorTests
  {
    private DateRangeCalculator dateRangeCalculator;
    private TimeSpan startTime;
    private TimeSpan endTime;
    
    [SetUp]
    public void Setup()
    {
      startTime = new TimeSpan(17, 30, 00);
      endTime = new TimeSpan(18, 00, 00);

      // Wednesday 14th January 2015 @3pm
      var clock = new TestClock(new DateTime(2015, 1, 14, 12, 0, 0));
      
      dateRangeCalculator = new DateRangeCalculator(clock);
    }

    [Test]
    public void DateBeforeWednesday_ChoosesNextWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Monday, startTime, DayOfWeek.Monday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Sunday 6pm - Monday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 18, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 19, 18, 0, 0), range.End);
    }

    [Test]
    public void DateAfterWednesday_ChoosesSameWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Friday, startTime, DayOfWeek.Friday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Thursday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 15, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateIsWednesday_ChoosesSameWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Wednesday, startTime, DayOfWeek.Wednesday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Thursday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 13, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 14, 18, 0, 0), range.End);
    }
    
    [Test]
    public void DateRangeAfterWednesday_SpanningMultipleDays_GivesExpectedRangeInSameWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Thursday, startTime, DayOfWeek.Friday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Wednesday 6pm - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 14, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeBeforeWednesday_SpanningMultipleDays_GivesExpectedRangeNextWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Monday, startTime, DayOfWeek.Tuesday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Sunday 6pm - Tuesday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 18, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 20, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeStartingWednesday_SelectsThisWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Wednesday, startTime, DayOfWeek.Friday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Tuesday 6pm (yesterday!) - Friday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 13, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 16, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeEndingOnWednesday_SelectsNextWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Monday, startTime, DayOfWeek.Wednesday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Sunday 6pm (next week) - Wednesday 6pm (next week)
      Assert.AreEqual(new DateTime(2015, 1, 18, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 21, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeSpanningBetweenWednesday_SelectsThisWeek()
    {
      var schedule = new OofSchedule(DayOfWeek.Tuesday, startTime, DayOfWeek.Thursday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Monday (in the past) 6pm - Thursday 6pm
      Assert.AreEqual(new DateTime(2015, 1, 12, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 15, 18, 0, 0), range.End);
    }

    [Test]
    public void DateRangeSpanningWeekend_GivesExpectedRange()
    {
      var schedule = new OofSchedule(DayOfWeek.Friday, startTime, DayOfWeek.Monday, endTime);
      var range = dateRangeCalculator.CalculateNextDateRangeForOof(schedule);

      // Thursday - Monday the next week
      Assert.AreEqual(new DateTime(2015, 1, 15, 17, 30, 0), range.Start);
      Assert.AreEqual(new DateTime(2015, 1, 19, 18, 0, 00), range.End);
    }
  }
}