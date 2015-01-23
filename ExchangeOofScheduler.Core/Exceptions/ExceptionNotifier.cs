using ExchangeOofScheduler.Core.Exchange;
using System;

namespace ExchangeOofScheduler.Core.Exceptions
{
  /// <summary>
  /// Handles exceptions raised by OofSetter
  /// </summary>
  public class ExceptionNotifier : IExceptionNotifier
  {
    private readonly IExchangeClient exchangeClient;
    private readonly IApplicationSettings settings;
    private const string subject = "ERROR auto-setting Out Of Office";

    public ExceptionNotifier(IExchangeClient exchangeClient, IApplicationSettings settings)
    {
      this.exchangeClient = exchangeClient;
      this.settings = settings;
    }

    public void HandleException(Exception e)
    {
      var body = GetMessageBody(e);
      exchangeClient.SendEmailToSelf(subject, body);

      if (settings.DebugModeEnabled)
      {
        Console.WriteLine(body);
      }
    }

    private string GetMessageBody(Exception e)
    {
      if (e is OofAlreadyEnabledException)
      {
        var exception = e as OofAlreadyEnabledException;
        return exception.Body;
      }

      return e.ToString().Replace(Environment.NewLine, "<br />");
    }
  }
}