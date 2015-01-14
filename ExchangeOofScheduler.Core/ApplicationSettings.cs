using ExchangeOofScheduler.Core.Dates;
using System;
using System.Collections.Specialized;
using System.Configuration;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core
{
  public class ApplicationSettings
  {
    private NameValueCollection applicationSettings;
    private readonly DayOfWeekReader DayOfWeekReader;
    private readonly DateRangeCalculator dateRangeCalculator;

    public ApplicationSettings(DayOfWeekReader dayOfWeekReader, DateRangeCalculator dateRangeCalculator)
    {
      applicationSettings = ConfigurationManager.GetSection("OutOfOfficeSettings") as NameValueCollection;
      this.DayOfWeekReader = dayOfWeekReader;
      this.dateRangeCalculator = dateRangeCalculator;
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

    public DayOfWeek endDay
    {
      get { return DayOfWeekReader.Read(applicationSettings["endDay"]); }
    }

    public DateRange nextApplicableDateRangeForOof
    {
      get { return dateRangeCalculator.CalculateNextDateRangeForOof(startDay, endDay); }
    }

    public string internalReply
    {
      get { return applicationSettings["internalReply"]; }
    }

    public OofExternalAudience sendToExternalRecipients
    {
      get
      {
        var configValue = applicationSettings["sendToExternalRecipients"].ToLower();
        switch (configValue)
        {
          case "none":
            return OofExternalAudience.None;
          case "known":
            return OofExternalAudience.Known;
          case "all":
            return OofExternalAudience.All;
          default:
            var message =
              string.Format(
                "Unexpected value for \"sendToExternalRecipients\" ({0}). Expected values are None,All and Known.",
                configValue);
            throw new ArgumentException(message);
        }
      }
    }

    public string externalReply
    {
      get { return applicationSettings["externalReply"]; }
    }
  }
}
