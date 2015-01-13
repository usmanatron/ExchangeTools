using System;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeOofScheduler.Core
{
  public class OofAlreadyEnabledException : Exception
  {
    private OofSettings enabledOof;
    private const string dateFormat = "ddd dd MMM yyyy HH:mm:ss";

    public OofAlreadyEnabledException(OofSettings enabledOof)
    {
      this.enabledOof = enabledOof;
    }

    public string EnabledTimePeriod
    {
      get
      {
        return string.Format("{0} - {1}", 
          enabledOof.Duration.StartTime.ToString(dateFormat),
          enabledOof.Duration.EndTime.ToString(dateFormat));
      }
    }
  }
}
