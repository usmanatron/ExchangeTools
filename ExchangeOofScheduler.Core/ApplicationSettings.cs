using System.Collections.Specialized;
using System.Configuration;

namespace ExchangeOofScheduler.Core
{
  public class ApplicationSettings
  {
    private static NameValueCollection applicationSettings;

    public ApplicationSettings()
    {
      applicationSettings = ConfigurationManager.GetSection("OutOfOfficeSettings") as NameValueCollection;
    }

    public string userEmail
    {
      get
      {
        return applicationSettings["userEmail"];
      }
    }

    public bool debugModeEnabled
    {
      get { return bool.Parse(applicationSettings["debugModeEnabled"]); }
    }
  }
}
