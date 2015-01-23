using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core.Exchange
{
  /// <remarks>
  ///   Suppress NCrunch testing on this class, as it requires a connection to Exchange (which we currently
  ///   cannot fake easily). This has also been suppressed when generating Opencover reports
  /// </remarks>
  //ncrunch: no coverage start
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

      if (this.settings.DebugModeEnabled)
      {
        service.TraceEnabled = true;
        service.TraceFlags = TraceFlags.All;
      }

      service.AutodiscoverUrl(this.settings.UserEmail, ValidateRedirectionUrl);
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
      return exchangeService.GetUserOofSettings(settings.UserEmail);
    }

    public void SetOofSettings(OofSettings oofSettings)
    {
      exchangeService.SetUserOofSettings(settings.UserEmail, oofSettings);
    }

    public void SendEmailToSelf(string subject, string body)
    {
      var message = new EmailMessage(exchangeService)
              {
                From = settings.UserEmail,
                Subject = subject,
                Body = new MessageBody(BodyType.HTML, body)
              };
      message.ToRecipients.Add(settings.UserEmail);

      message.Send();
    }
  }

  //ncrunch: no coverage end
}