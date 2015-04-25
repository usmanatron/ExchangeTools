using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Exceptions;
using System;

namespace ExchangeOofScheduler
{
  public class OofScheduler
  {
    private readonly IOutOfOfficeSetter oofSetter;
    private readonly IExceptionHandler exceptionHandler;
    private readonly IApplicationSettings settings;

    public OofScheduler(IOutOfOfficeSetter oofSetter, IExceptionHandler exceptionHandler, IApplicationSettings settings)
    {
      this.oofSetter = oofSetter;
      this.exceptionHandler = exceptionHandler;
      this.settings = settings;
    }

    public void ScheduleOof()
    {
      try
      {
        oofSetter.SetOutOfOffice();
      }
      catch (Exception exception)
      {
        var messageBody = exceptionHandler.GetExceptionMessage(exception);

        exceptionHandler.EmailException(messageBody);

        if (settings.DebugModeEnabled)
        {
          exceptionHandler.WriteExceptionToConsole(messageBody);
        }
      }
    }
  }
}