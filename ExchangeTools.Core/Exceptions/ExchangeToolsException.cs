using System;

namespace ExchangeTools.Core.Exceptions
{
  /// <summary>
  /// Custom Exception raised by an ExchangeTools application
  /// </summary>
  [Serializable]
  public abstract class ExchangeToolsException : Exception
  {
    public abstract string Body { get; }
  }
}