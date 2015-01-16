using System;
using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Exceptions;

namespace ExchangeOofScheduler
{
  public class OofScheduler
  {
    private readonly IOutOfOfficeSetter oofSetter;
    private readonly IExceptionNotifier oofSetterExceptionHandler;

    public OofScheduler(IOutOfOfficeSetter oofSetter, IExceptionNotifier oofSetterExceptionHandler)
    {
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
    }
  }
}