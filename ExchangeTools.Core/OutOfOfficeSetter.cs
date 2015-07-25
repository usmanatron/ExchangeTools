using ExchangeTools.Core.Builders;
using ExchangeTools.Core.Exceptions;
using ExchangeTools.Core.Exchange;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeTools.Core
{
  /// <summary>
  /// Handles the actual setting of an out of office message
  /// </summary>
  public class OutOfOfficeSetter : IOutOfOfficeSetter
  {
    private readonly IExchangeClient exchangeClient;
    private readonly IOofSettingsBuilder oofSettingsBuilder;

    public OutOfOfficeSetter(IExchangeClient exchangeClient, IOofSettingsBuilder oofSettingsBuilder)
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