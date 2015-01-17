using System.Collections.Specialized;
using System.Configuration;

namespace ExchangeOofScheduler.Core
{
  public class ApplicationSettings : IApplicationSettings
  {
    private readonly NameValueCollection applicationSettings;
    private readonly OofScheduleBuilder oofScheduleBuilder;

    public ApplicationSettings(OofScheduleBuilder oofScheduleBuilder)
    {
      applicationSettings = ConfigurationManager.GetSection("OutOfOfficeSettings") as NameValueCollection;
      this.oofScheduleBuilder = oofScheduleBuilder;
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

    public OofSchedule oofSchedule
    {
      get
      {
        return oofScheduleBuilder.Build(applicationSettings["startDay"],
                               applicationSettings["boundaryTime"],
                               applicationSettings["endDay"],
                               applicationSettings["boundaryTime"]);
      }
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
