using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Exchange;
using Ninject;

namespace ExchangeOofScheduler
{
  class Program
  {
    static void Main()
    {
      var kernel = SetupNinjectKernel();
      kernel.Get<OofScheduler>().ScheduleOof();
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