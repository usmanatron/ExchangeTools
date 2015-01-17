using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class ExchangeClient : IExchangeClient
  {
    private readonly ExchangeService exchangeService;
    private readonly IApplicationSettings settings;

    public ExchangeClient(ExchangeServiceBuilder exchangeServiceBuilder, IApplicationSettings settings)
    {
      this.settings = settings;
      this.exchangeService = exchangeServiceBuilder
        .WithEmail(settings.userEmail)
        .WithTracing(settings.debugModeEnabled)
        .Build();
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