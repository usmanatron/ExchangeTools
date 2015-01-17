using System;
using ExchangeOofScheduler.Core.Dates;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class OofSettingsBuilder : IOofSettingsBuilder
  {
    private readonly OofSettings oofSettings;
    private readonly IApplicationSettings applicationSettings;
    private readonly DateRangeCalculator dateRangeCalculator;

    public OofSettingsBuilder(IApplicationSettings applicationSettings, DateRangeCalculator dateRangeCalculator)
    {
      this.oofSettings = new OofSettings{ State = OofState.Scheduled };
      this.applicationSettings = applicationSettings;
      this.dateRangeCalculator = dateRangeCalculator;
    }

    public OofSettings Build()
    {
      var nextDateRange = dateRangeCalculator.CalculateNextDateRangeForOof(applicationSettings.startDay, applicationSettings.startTime, applicationSettings.endDay, applicationSettings.endTime);
      oofSettings.Duration = new TimeWindow(nextDateRange.Start, nextDateRange.End);
      oofSettings.ExternalReply = new OofReply(applicationSettings.externalReply);
      oofSettings.InternalReply = new OofReply(applicationSettings.internalReply);
      oofSettings.ExternalAudience = GetExternalAudienceSetting();
      return oofSettings;
    }

    private OofExternalAudience GetExternalAudienceSetting()
    {
      var configValue = applicationSettings.sendToExternalRecipients.ToLower();
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
}