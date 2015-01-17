using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public interface IExchangeClient
  {
    OofSettings GetOofSettings();

    void SetOofSettings(OofSettings oofSettings);

    void SendEmailToSelf(string subject, string body);
  }
}