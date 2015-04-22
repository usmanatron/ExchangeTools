using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Config;
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
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
      }
    }

    private static StandardKernel SetupNinjectKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IClock>().To<SystemClock>();
      kernel.Bind<IApplicationSettings>().To<ApplicationSettings>();
      kernel.Bind<IExceptionNotifier>().To<ExceptionNotifier>();
      kernel.Bind<IOofSettingsBuilder>().To<OofSettingsBuilder>();
      kernel.Bind<IOofScheduleBuilder>().To<OofScheduleBuilder>();
      kernel.Bind<IOutOfOfficeSetter>().To<OutOfOfficeSetter>();
      kernel.Bind<IDayOfWeekReader>().To<DayOfWeekReader>();
      kernel.Bind<IDateRangeCalculator>().To<DateRangeCalculator>();
      kernel.Bind<IConfiguration>().To<ApplicationConfiguration>();
      kernel.Bind<IExchangeClient>().To<ExchangeClient>().InSingletonScope();
      kernel.Bind<ApplicationSettings>().ToSelf().InSingletonScope();
      return kernel;
    }
  }
}