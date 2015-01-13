using ExchangeOofScheduler.Core.Exchange;
using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core
{
  /// <summary>
  /// Handles the actual setting of an out of office message
  /// </summary>
  public class OutOfOfficeSetter
  {
    private readonly ExchangeClient exchangeClient;
    private readonly OofSettingsBuilder oofSettingsBuilder;

    public OutOfOfficeSetter(ExchangeClient exchangeClient, OofSettingsBuilder oofSettingsBuilder)
    {
      this.exchangeClient = exchangeClient;
      this.oofSettingsBuilder = oofSettingsBuilder;
    }

    /// <summary>
    /// First retrieve the current OOF. If one is already enabled, terminate with an error (i.e. assume it was intentional).
    /// Otherwise, re-set with the information given in the application config.
    /// </summary>
    public void SetOutOfOffice()
    {
      var currentSettings = exchangeClient.GetOofSettings();

      if (currentSettings.State != OofState.Disabled)
      {
        throw new OofAlreadyEnabledException(currentSettings);
      }

      var newSettings = oofSettingsBuilder.Build();
      exchangeClient.SetOofSettings(newSettings);
    }
  }
}
