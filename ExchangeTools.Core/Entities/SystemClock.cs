using System;

namespace ExchangeTools.Core.Entities
{
  /// <summary>
  /// Idea from: http://stackoverflow.com/questions/43711/whats-a-good-way-to-overwrite-datetime-now-during-testing
  /// </summary>
  public interface IClock
  {
    DateTime Now { get; }
  }

  public class SystemClock : IClock
  {
    public DateTime Now
    {
      get { return DateTime.Now; }
    }
  }
}