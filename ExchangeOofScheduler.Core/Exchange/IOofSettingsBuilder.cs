using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public interface IOofSettingsBuilder
  {
    OofSettings Build();
  }
}