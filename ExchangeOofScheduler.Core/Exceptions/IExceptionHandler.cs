using System;

namespace ExchangeTools.Core.Exceptions
{
  public interface IExceptionHandler
  {
    string GetExceptionMessage(Exception e);

    void EmailException(string messageBody);

    void WriteExceptionToConsole(string details);
  }
}