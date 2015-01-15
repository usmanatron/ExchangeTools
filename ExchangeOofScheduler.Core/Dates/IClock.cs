﻿using System;

namespace ExchangeOofScheduler.Core.Dates
{
  /// <summary>
  /// Idea from: http://stackoverflow.com/questions/43711/whats-a-good-way-to-overwrite-datetime-now-during-testing
  /// </summary>
  public interface IClock
  {
    DateTime Now { get; }
  }
}