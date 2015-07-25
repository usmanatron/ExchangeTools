using System.Collections.Generic;

namespace ExchangeTools.Core.Config
{
  public interface IConfiguration
  {
    IDictionary<string, string> Config { get; }
  }
}