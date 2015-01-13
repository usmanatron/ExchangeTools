using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core
{
  public class ExchangeClient
  {
    public void Test()
    {
      var service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
      service.UseDefaultCredentials = true;
      service.AutodiscoverUrl("usman.iqbal@softwire.com", RedirectionUrlValidationCallback);

      service.TraceEnabled = true;
      service.TraceFlags = TraceFlags.All;

      var a = service.GetUserOofSettings("usman.iqbal@softwire.com");
      Console.WriteLine(a);
    }

    private static bool RedirectionUrlValidationCallback(string redirectionUrl)
    {
      // The default for the validation callback is to reject the URL.
      bool result = false;

      Uri redirectionUri = new Uri(redirectionUrl);

      // Validate the contents of the redirection URL. In this simple validation
      // callback, the redirection URL is considered valid if it is using HTTPS
      // to encrypt the authentication credentials. 
      if (redirectionUri.Scheme == "https")
      {
        result = true;
      }
      return result;
    }
  }
}
