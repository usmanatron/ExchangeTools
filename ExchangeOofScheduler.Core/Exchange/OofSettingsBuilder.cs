using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class OofSettingsBuilder
  {
    private readonly OofSettings oofSettings;
    private readonly ApplicationSettings applicationSettings;

    public OofSettingsBuilder(ApplicationSettings applicationSettings)
    {
      this.oofSettings = new OofSettings();
      this.applicationSettings = applicationSettings;
    }

    public OofSettings Build()
    {
      //qqUMI Fill this in once application settings is done!
      return oofSettings;
    }
  }
}
