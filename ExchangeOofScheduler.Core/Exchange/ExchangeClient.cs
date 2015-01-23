using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class ExchangeClient : IExchangeClient
  {
    private readonly ExchangeService exchangeService;
    private readonly IApplicationSettings settings;

    public ExchangeClient(IApplicationSettings settings)
    {
      this.settings = settings;
      this.exchangeService = BuildService();
    }

    private ExchangeService BuildService()
    {
      var service = new ExchangeService { UseDefaultCredentials = true };

      if (this.settings.debugModeEnabled)
      {
        service.TraceEnabled = true;
        service.TraceFlags = TraceFlags.All;
      }

      service.AutodiscoverUrl(this.settings.userEmail, ValidateRedirectionUrl);
      return service;
    }

    /// <summary>
    /// Validate the contents of the redirection URL. In this simple validation
    /// callback, the redirection URL is considered valid if it is using HTTPS
    /// to encrypt the authentication credentials.
    /// </summary>
    private bool ValidateRedirectionUrl(string redirectionUrl)
    {
      var redirectionUri = new Uri(redirectionUrl);
      return redirectionUri.Scheme == "https";
    }

    public OofSettings GetOofSettings()
    {
      return exchangeService.GetUserOofSettings(settings.userEmail);
    }

    public void SetOofSettings(OofSettings oofSettings)
    {
      exchangeService.SetUserOofSettings(settings.userEmail, oofSettings);
    }

    public void SendEmailToSelf(string subject, string body)
    {
      var message = new EmailMessage(exchangeService)
              {
                From = settings.userEmail,
                Subject = subject,
                Body = new MessageBody(BodyType.HTML, body)
              };
      message.ToRecipients.Add(settings.userEmail);

      message.Send();
    }
  }
}