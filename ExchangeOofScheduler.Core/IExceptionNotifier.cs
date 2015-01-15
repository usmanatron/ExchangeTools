using System;

namespace ExchangeOofScheduler.Core
{
  public interface IExceptionNotifier
  {
    void HandleException(Exception e);
  }
}
