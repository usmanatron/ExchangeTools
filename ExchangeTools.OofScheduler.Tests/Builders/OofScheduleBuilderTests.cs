using ExchangeTools.Core.Dates;
using ExchangeTools.OofScheduler.Builders;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace ExchangeTools.OofScheduler.Tests.Builders
{
  [TestFixture]
  internal class OofScheduleBuilderTests
  {
    private IDayOfWeekReader dayOfWeekReader;
    private OofScheduleBuilder oofScheduleBuilder;

    private const string startDay = "startDay";
    private const string startTime = "17:30:00";
    private const string endDay = "endDay";
    private const string endTime = "17:30:00";

    [SetUp]
    public void Setup()
    {
      dayOfWeekReader = A.Fake<IDayOfWeekReader>();
      A.CallTo(() => dayOfWeekReader.Read(startDay)).Returns(DayOfWeek.Thursday);
      A.CallTo(() => dayOfWeekReader.Read(endDay)).Returns(DayOfWeek.Friday);

      this.oofScheduleBuilder = new OofScheduleBuilder(dayOfWeekReader);
    }

    [Test]
    public void StartAndEndDates_SetCorrectly()
    {
      var schedule = oofScheduleBuilder.Build(startDay, startTime, endDay, endTime);

      Assert.AreEqual(DayOfWeek.Thursday, schedule.StartDay);
      Assert.AreEqual(DayOfWeek.Friday, schedule.EndDay);
    }

    [Test]
    [TestCase("invalidTime!")]
    [TestCase("")]
    [TestCase("12:70:00")] //Invalid minutes
    public void InvalidStartAndEndTimes_Throw(string time)
    {
      Assert.Throws<FormatException>(() => oofScheduleBuilder.Build(startDay, time, endDay, endTime));
      Assert.Throws<FormatException>(() => oofScheduleBuilder.Build(startDay, startTime, endDay, time));
    }
  }
}