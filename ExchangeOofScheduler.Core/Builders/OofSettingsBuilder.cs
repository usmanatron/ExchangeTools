using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Exchange;
using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core.Builders
{
  public class OofSettingsBuilder : IOofSettingsBuilder
  {
    private readonly OofSettings oofSettings;
    private readonly IApplicationSettings applicationSettings;
    private readonly IDateRangeCalculator dateRangeCalculator;
    private readonly IOofScheduleBuilder oofScheduleBuilder;
    private readonly IFileReader fileReader;

    public OofSettingsBuilder(IApplicationSettings applicationSettings, IDateRangeCalculator dateRangeCalculator, IOofScheduleBuilder oofScheduleBuilder, IFileReader fileReader)
    {
      this.applicationSettings = applicationSettings;
      this.dateRangeCalculator = dateRangeCalculator;
      this.oofScheduleBuilder = oofScheduleBuilder;
      this.fileReader = fileReader;
      oofSettings = new OofSettings { State = OofState.Scheduled };
    }

    public OofSettings Build()
    {
      var oofSchedule = oofScheduleBuilder.Build(
        applicationSettings.StartDay,
        applicationSettings.BoundaryTime,
        applicationSettings.EndDay,
        applicationSettings.BoundaryTime);

      var dateRange = dateRangeCalculator.CalculateNextDateRangeForOof(oofSchedule);
      oofSettings.Duration = new TimeWindow(dateRange.Start, dateRange.End);
      oofSettings.ExternalReply = new OofReply(fileReader.GetFileContents(applicationSettings.ExternalReplyFilename));
      oofSettings.InternalReply = new OofReply(fileReader.GetFileContents(applicationSettings.InternalReplyFilename));
      oofSettings.ExternalAudience = GetExternalAudienceSetting();
      return oofSettings;
    }

    private OofExternalAudience GetExternalAudienceSetting()
    {
      var configValue = applicationSettings.SendToExternalRecipients.ToLower();
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
              "Unexpected value for \"SendToExternalRecipients\" ({0}). Expected values are None, All and Known.",
              configValue);
          throw new ArgumentException(message);
      }
    }
  }
}