using ExchangeTools.Core;
using ExchangeTools.Core.Builders;
using ExchangeTools.Core.Config;
using ExchangeTools.Core.Dates;
using ExchangeTools.Core.Entities;
using ExchangeTools.Core.Exceptions;
using ExchangeTools.Core.Exchange;
using Ninject;
using System;

namespace ExchangeOofScheduler
{
  public class Program
  {
    public static void Main()
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
      kernel.Bind<IExceptionHandler>().To<ExceptionHandler>();
      kernel.Bind<IOofSettingsBuilder>().To<OofSettingsBuilder>();
      kernel.Bind<IOofScheduleBuilder>().To<OofScheduleBuilder>();
      kernel.Bind<IFileReader>().To<FileReader>();
      kernel.Bind<IOutOfOfficeSetter>().To<OutOfOfficeSetter>();
      kernel.Bind<IDayOfWeekReader>().To<DayOfWeekReader>();
      kernel.Bind<IDateRangeCalculator>().To<DateRangeCalculator>();
      kernel.Bind<IConfiguration>().To<ApplicationConfiguration>();
      kernel.Bind<IExchangeClient>().To<ExchangeClient>().InSingletonScope();
      kernel.Bind<ApplicationSettings>().ToSelf().InSingletonScope();
      kernel.Bind<IMailMessageBuilder>().To<MailMessageBuilder>();
      return kernel;
    }
  }
}