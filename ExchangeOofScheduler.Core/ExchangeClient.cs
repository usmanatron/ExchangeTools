using System;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core
{
  public class ExchangeClient : IExchangeClient
  {
    private readonly ExchangeService exchangeService;
    private readonly ExchangeSettings settings;

    public ExchangeClient(ExchangeServiceBuilder exchangeServiceBuilder, ExchangeSettings settings)
    {
      this.settings = settings;
      this.exchangeService = exchangeServiceBuilder
        .WithEmail(settings.userEmail)
        .WithTracingEnabled()
        .Build();
    }

    public OofSettings GetOofSettings()
    {
      return exchangeService.GetUserOofSettings(settings.userEmail);
    }

    public bool TrySetOofSettings()
    {
      throw new NotImplementedException();
    }
  }
}
