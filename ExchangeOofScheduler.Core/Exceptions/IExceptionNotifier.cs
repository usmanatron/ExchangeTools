using System;

namespace ExchangeOofScheduler.Core.Exceptions
{
  public interface IExceptionNotifier
  {
    void HandleException(Exception e);
  }
}