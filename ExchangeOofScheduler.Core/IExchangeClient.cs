using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core
{
  public interface IExchangeClient
  {
    OofSettings GetOofSettings();

    bool TrySetOofSettings();
  }
}
