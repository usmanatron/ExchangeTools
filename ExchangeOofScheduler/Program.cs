using ExchangeOofScheduler.Core;
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
      kernel.Bind<IExchangeClient>().To<ExchangeClient>().InSingletonScope();
      kernel.Bind<ApplicationSettings>().ToSelf().InSingletonScope();
      return kernel;
    }
  }
}