using System;
using System.Linq.Expressions;

namespace ExchangeOofScheduler.Core.Tests.Config
{
  internal class StringSettingsTestCase
  {
    public Expression<Func<string>> FakeCall;
    public string Value;
    public Func<string> CheckFunc;

    public StringSettingsTestCase(Expression<Func<string>> fakeCall, string value, Func<string> checkFunc)
    {
      this.FakeCall = fakeCall;
      this.Value = value;
      this.CheckFunc = checkFunc;
    }
  }
}