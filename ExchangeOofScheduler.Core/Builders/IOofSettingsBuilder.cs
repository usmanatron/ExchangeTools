using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Builders
{
  public interface IOofSettingsBuilder
  {
    OofSettings Build();
  }
}