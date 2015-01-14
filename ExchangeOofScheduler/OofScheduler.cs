using System;
using ExchangeOofScheduler.Core;

namespace ExchangeOofScheduler
{
  public class OofScheduler
  {
    private readonly ApplicationSettings settings;
    private readonly OutOfOfficeSetter oofSetter;
    private readonly OofSetterExceptionHandler oofSetterExceptionHandler;

    public OofScheduler(ApplicationSettings settings, OutOfOfficeSetter oofSetter, OofSetterExceptionHandler oofSetterExceptionHandler)
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