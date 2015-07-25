using ExchangeTools.OofScheduler.Dates;
using NUnit.Framework;
using System;

namespace ExchangeTools.OofScheduler.Tests.Dates
{
  [TestFixture]
  internal class DayOfWeekReaderTests
  {
    private DayOfWeekReader reader;

    [SetUp]
    public void Setup()
    {
      reader = new DayOfWeekReader();
    }

    [Test]
    [TestCaseSource("validInputs")]
    public void ValidInput_ReturnsExpectedDayOfWeek(Tuple<string, DayOfWeek> input)
    {
      var output = reader.Read(input.Item1);

      Assert.AreEqual(input.Item2, output);
    }

    [Test]
    [TestCase("M")]
    [TestCase("Mon")]
    [TestCase("Monday")]
    [TestCase("")]
    public void InputWithWrongLength_Throws(string input)
    {
      Assert.Throws<ArgumentException>(() => reader.Read(input));
    }

    [Test]
    [TestCase("Bo")]
    [TestCase("?!")]
    public void UnexpectedInput_Throws(string input)
    {
      Assert.Throws<InvalidOperationException>(() => reader.Read(input));
    }

    private readonly object[] validInputs =
    {
      new Tuple<string, DayOfWeek>("Mo", DayOfWeek.Monday),
      new Tuple<string, DayOfWeek>("Tu", DayOfWeek.Tuesday),
      new Tuple<string, DayOfWeek>("We", DayOfWeek.Wednesday),
      new Tuple<string, DayOfWeek>("Th", DayOfWeek.Thursday),
      new Tuple<string, DayOfWeek>("Fr", DayOfWeek.Friday),
      new Tuple<string, DayOfWeek>("Sa", DayOfWeek.Saturday),
      new Tuple<string, DayOfWeek>("Su", DayOfWeek.Sunday)
    };
  }
}