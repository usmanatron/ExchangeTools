﻿using System;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class ExchangeClient : IExchangeClient
  {
    private readonly ExchangeService exchangeService;
    private readonly ApplicationSettings settings;

    public ExchangeClient(ExchangeServiceBuilder exchangeServiceBuilder, ApplicationSettings settings)
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
      //For now, whilst we get everything else setup, we forcibly stop this part from happening!
      //exchangeService.SetUserOofSettings(settings.userEmail, oofSettings);
    }
  }
}