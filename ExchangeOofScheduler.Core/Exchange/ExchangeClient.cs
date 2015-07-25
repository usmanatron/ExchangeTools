using ExchangeTools.Core.Config;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;

namespace ExchangeTools.Core.Exchange
{
  /// <remarks>
  ///   Suppress NCrunch testing on this class, as it requires a connection to Exchange (which we currently
  ///   cannot fake easily). This has also been suppressed when generating Opencover reports
  /// </remarks>
  //ncrunch: no coverage start
  [ExcludeFromCodeCoverage]
  public class ExchangeClient : IExchangeClient
  {
    private readonly ExchangeService exchangeService;
    private readonly IApplicationSettings settings;

    public ExchangeClient(IApplicationSettings settings)
    {
      this.settings = settings;
      exchangeService = BuildService();
    }

    private ExchangeService BuildService()
    {
      var service = new ExchangeService { UseDefaultCredentials = true };
      service.AutodiscoverUrl(settings.UserEmail, ValidateRedirectionUrl);

      if (settings.DebugModeEnabled)
      {
        service.TraceEnabled = true;
        service.TraceFlags = TraceFlags.All;
      }

      return service;
    }

    /// <summary>
    /// Validate the contents of the redirection URL.
    /// The redirection URL is considered valid if it is using HTTPS to encrypt
    /// the authentication credentials.
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

    public void SendEmailToSelf(MailMessage mailMessage)
    {
      var message = new EmailMessage(exchangeService)
              {
                From = mailMessage.Sender.Address,
                Subject = mailMessage.Subject,
                Body = new MessageBody(BodyType.HTML, mailMessage.Body)
              };

      message.ToRecipients.AddRange(
        mailMessage.To.Select(recipient => new EmailAddress(recipient.DisplayName, recipient.Address)));

      message.Send();
    }
  }

  //ncrunch: no coverage end
}