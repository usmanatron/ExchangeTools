using System;
using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Exceptions;
using ExchangeOofScheduler.Core.Exchange;
using Ninject;

namespace ExchangeOofScheduler
{
  class Program
  {
    static void Main()
    {
      var kernel = SetupNinjectKernel();
      var isDebugMode = kernel.Get<ApplicationSettings>().debugModeEnabled;
      
      kernel.Get<OofScheduler>().ScheduleOof();
      if (isDebugMode)
      {
        Console.ReadLine();
      }
    }

    private static StandardKernel SetupNinjectKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IClock>().To<SystemClock>();
      kernel.Bind<IApplicationSettings>().To<ApplicationSettings>();
      kernel.Bind<IExceptionNotifier>().To<ExceptionNotifier>();
      kernel.Bind<IExchangeClient>().To<ExchangeClient>().InSingletonScope();
      kernel.Bind<ApplicationSettings>().ToSelf().InSingletonScope();
      return kernel;
    }
  }
}