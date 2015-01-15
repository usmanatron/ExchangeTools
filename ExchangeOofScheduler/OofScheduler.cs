using System;
using ExchangeOofScheduler.Core;

namespace ExchangeOofScheduler
{
  public class OofScheduler
  {
    private readonly IApplicationSettings settings;
    private readonly OutOfOfficeSetter oofSetter;
    private readonly IExceptionNotifier oofSetterExceptionHandler;

    public OofScheduler(IApplicationSettings settings, OutOfOfficeSetter oofSetter, IExceptionNotifier oofSetterExceptionHandler)
    {
      this.settings = settings;
      this.oofSetter = oofSetter;
      this.oofSetterExceptionHandler = oofSetterExceptionHandler;
    }

    public void ScheduleOof()
    {
      try
      {
        oofSetter.SetOutOfOffice();
      }
      catch (Exception exception)
      {
        oofSetterExceptionHandler.HandleException(exception);
      }

      // Pause if debug mode is enabled, so we can read the Exchange log messages
      if (settings.debugModeEnabled)
      {
        Console.ReadLine();
      }
    }
  }
}