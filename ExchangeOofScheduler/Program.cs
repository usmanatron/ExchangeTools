using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Exceptions;
using ExchangeOofScheduler.Core.Exchange;
using Ninject;
using System;

namespace ExchangeOofScheduler
{
  internal class Program
  {
    private static void Main()
    {
      var kernel = SetupNinjectKernel();
      var isDebugMode = kernel.Get<ApplicationSettings>().DebugModeEnabled;

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