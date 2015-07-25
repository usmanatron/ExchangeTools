using Microsoft.Exchange.WebServices.Data;
using System.Net.Mail;

namespace ExchangeTools.Core.Exchange
{
  public interface IExchangeClient
  {
    OofSettings GetOofSettings();

    void SetOofSettings(OofSettings oofSettings);

    void SendEmailToSelf(MailMessage mailMessage);
  }
}