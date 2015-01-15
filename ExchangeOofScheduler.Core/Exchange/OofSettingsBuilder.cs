using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class OofSettingsBuilder
  {
    private readonly OofSettings oofSettings;
    private readonly IApplicationSettings applicationSettings;

    public OofSettingsBuilder(IApplicationSettings applicationSettings)
    {
      this.oofSettings = new OofSettings();
      this.applicationSettings = applicationSettings;
    }

    public OofSettings Build()
    {
      var nextDateRange = applicationSettings.nextApplicableDateRangeForOof;
      oofSettings.Duration = new TimeWindow(nextDateRange.Start, nextDateRange.End);
      oofSettings.State = OofState.Scheduled;
      oofSettings.ExternalReply = new OofReply(applicationSettings.externalReply);
      oofSettings.InternalReply = new OofReply(applicationSettings.internalReply);
      oofSettings.ExternalAudience = applicationSettings.sendToExternalRecipients;
      return oofSettings;
    }
  }
}
