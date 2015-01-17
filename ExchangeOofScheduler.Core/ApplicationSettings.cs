using ExchangeOofScheduler.Core.Dates;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace ExchangeOofScheduler.Core
{
  public class ApplicationSettings : IApplicationSettings
  {
    private readonly NameValueCollection applicationSettings;
    private readonly DayOfWeekReader DayOfWeekReader;


    public ApplicationSettings(DayOfWeekReader dayOfWeekReader)
    {
      applicationSettings = ConfigurationManager.GetSection("OutOfOfficeSettings") as NameValueCollection;
      this.DayOfWeekReader = dayOfWeekReader;
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

    public DayOfWeek startDay
    {
      get { return DayOfWeekReader.Read(applicationSettings["startDay"]); }
    }

    public TimeSpan startTime
    {
      get { return DateTime.Parse(applicationSettings["startTime"]).TimeOfDay; }
    }

    public DayOfWeek endDay
    {
      get { return DayOfWeekReader.Read(applicationSettings["endDay"]); }
    }

    public TimeSpan endTime
    {
      get { return DateTime.Parse(applicationSettings["endTime"]).TimeOfDay; }
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
