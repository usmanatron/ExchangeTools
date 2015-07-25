using ExchangeTools.Core.Config;
using System;

namespace ExchangeTools.Core.Exceptions
{
  public interface IExceptionHandler
  {
    string GetExceptionMessage(Exception e);

    void EmailException(string messageBody);

    void WriteExceptionToConsole(string details);
  }

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
      var exchangeToolsException = e as ExchangeToolsException;

      if (exchangeToolsException != null)
      {
        return exchangeToolsException.Body;
      }

      return e.ToString();
    }

    public void EmailException(string messageBody)
    {
      // We want a HTML body, so replace new lines with <br />
      messageBody = messageBody.Replace(Environment.NewLine, "<br />");

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