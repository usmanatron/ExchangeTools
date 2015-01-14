using System;
using ExchangeOofScheduler.Core.Exchange;

namespace ExchangeOofScheduler.Core
{
  /// <summary>
  /// Handles exceptions raised by OofSetter
  /// </summary>
  public class OofSetterExceptionHandler
  {
    private readonly ExchangeClient exchangeClient;
    private readonly ApplicationSettings settings;
    private const string subject = "ERROR auto-setting Out Of Office";

    public OofSetterExceptionHandler(ExchangeClient exchangeClient, ApplicationSettings settings)
    {
      this.exchangeClient = exchangeClient;
      this.settings = settings;
    }

    public void HandleException(Exception e)
    {
      var body = GetMessageBody(e);
      exchangeClient.SendEmailToSelf(subject, body);
      
      if (settings.debugModeEnabled)
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