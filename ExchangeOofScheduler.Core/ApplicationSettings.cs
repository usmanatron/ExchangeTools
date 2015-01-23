using System.Collections.Specialized;
using System.Configuration;

namespace ExchangeOofScheduler.Core
{
  public class ApplicationSettings : IApplicationSettings
  {
    private readonly NameValueCollection applicationSettings;

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

    public string startDay
    {
      get { return applicationSettings["startDay"]; }
    }

    public string boundaryTime
    {
      get { return applicationSettings["boundaryTime"]; }
    }

    public string endDay
    {
      get { return applicationSettings["endDay"]; }
    }

    public string internalReply
    {
      get { return applicationSettings["internalReply"]; }
    }

    public string sendToExternalRecipients
    {
      get
      {
        return applicationSettings["sendToExternalRecipients"];
      }
    }

    public string externalReply
    {
      get { return applicationSettings["externalReply"]; }
    }
  }
}