using System.Collections;
using System.Collections.Generic;

namespace ExchangeOofScheduler.Core.Config
{
  public interface IConfiguration
  {
    IDictionary<string, string> Config { get; }
  }
}