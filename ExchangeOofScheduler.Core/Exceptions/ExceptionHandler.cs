using ExchangeOofScheduler.Core.Builders;
using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Exchange;
using System;

namespace ExchangeOofScheduler.Core.Exceptions
{
  /// <summary>
  /// Handles exceptions raised by OofSetter
  /// </summary>
  public class ExceptionHandler : IExceptionHandler
  {
    private readonly IExchangeClient exchangeClient;
    private readonly IApplicationSettings settings;
    private readonly IMailMessageBuilder mailMessageBuilder;
    private const string Subject = "ERROR auto-setting Out Of Office";

    public ExceptionHandler(IExchangeClient exchangeClient, IApplicationSettings settings, IMailMessageBuilder mailMessageBuilder)
    {
      this.exchangeClient = exchangeClient;
      this.settings = settings;
      this.mailMessageBuilder = mailMessageBuilder;
    }

    public string GetExceptionMessage(Exception e)
    {
      if (e is OofAlreadyEnabledException)
      {
        var exception = e as OofAlreadyEnabledException;
        return exception.Body;
      }

      return e.ToString().Replace(Environment.NewLine, "<br />");
    }

    public void EmailException(string messageBody)
    {
      var mailMessage = mailMessageBuilder
        .WithSender(settings.UserEmail)
        .WithRecipient(settings.UserEmail)
        .WithSubject(Subject)
        .WithBody(messageBody)
        .Build();

      exchangeClient.SendEmailToSelf(mailMessage);
    }

    public void WriteExceptionToConsole(string details)
    {
      Console.WriteLine(details);
    }
  }
}