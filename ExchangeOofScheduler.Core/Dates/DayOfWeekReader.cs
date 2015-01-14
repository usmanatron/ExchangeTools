using System;

namespace ExchangeOofScheduler.Core.Dates
{
  public class DayOfWeekReader
  {
    private const int expectedInputLength = 2;

    public DayOfWeek Read(string input)
    {
      if (input.Length != expectedInputLength)
      {
        throw new ArgumentException("The given day of week (" + input + ") is not the expected length of " + expectedInputLength);
      }

      switch (input.ToLower())
      {
        case "mo":
          return DayOfWeek.Monday;
        case "tu":
          return DayOfWeek.Tuesday;
        case "we":
          return DayOfWeek.Wednesday;
        case "th":
          return DayOfWeek.Thursday;
        case "fr":
          return DayOfWeek.Friday;
        default:
          throw new InvalidOperationException("The given day of week (" + input + ") is not expected - please check and retry.");
      }
    }
  }
}