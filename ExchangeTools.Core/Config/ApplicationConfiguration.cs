using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace ExchangeTools.Core.Config
{
  public interface IConfiguration
  {
    IDictionary<string, string> Config { get; }
  }

  public class ApplicationConfiguration : IConfiguration
  {
    public IDictionary<string, string> Config
    {
      get
      {
        return ConfigurationManager.AppSettings
          .Cast<string>()
          .ToDictionary(entry => entry, entry => ConfigurationManager.AppSettings[entry]);
      }
    }
  }
}