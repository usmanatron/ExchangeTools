﻿using ExchangeTools.Core.Exceptions;
using System;

namespace ExchangeTools.Core.Tests.Exceptions
{
  [Serializable]
  internal class TestExchangeToolsException : ExchangeToolsException
  {
    public const string ExceptionMessage = @"An out of office has already been set";

    public override string Body
    {
      get { return ExceptionMessage; }
    }
  }
}