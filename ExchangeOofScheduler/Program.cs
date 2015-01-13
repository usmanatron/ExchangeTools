using System;
using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Exchange;
using Ninject;

namespace ExchangeOofScheduler
{
  class Program
  {
    static void Main(string[] args)
    {
      var kernel = SetupNinjectKernel();

      try
      {
        kernel.Get<OutOfOfficeSetter>().SetOutOfOffice();
      }
      catch (OofAlreadyEnabledException e)
      {
        Console.WriteLine("Out of office already set for " + e.EnabledTimePeriod + ".  This has ***NOT*** been overwritten!");
        Console.ReadLine();
      }
    }

    private static StandardKernel SetupNinjectKernel()
    {
      var kernel = new StandardKernel();
      kernel.Bind<IExchangeClient>().To<ExchangeClient>();
      kernel.Bind<ApplicationSettings>().ToSelf().InSingletonScope();
      return kernel;
    }
  }
}