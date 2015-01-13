using ExchangeOofScheduler.Core;
using Ninject;

namespace ExchangeOofScheduler
{
  class Program
  {
    static void Main(string[] args)
    {
      var kernel = SetupNinjectKernel();
      new ExchangeSettings();

      kernel.Get<IExchangeClient>().GetOofSettings();

    }

    private static StandardKernel SetupNinjectKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IExchangeClient>().To<ExchangeClient>();
      kernel.Bind<ExchangeSettings>().ToSelf().InSingletonScope();
      return kernel;
    }
  }
}
