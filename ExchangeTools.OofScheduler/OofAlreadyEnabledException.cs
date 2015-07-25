using ExchangeTools.Core.Exceptions;
using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeTools.OofScheduler
{
  [Serializable]
  public class OofAlreadyEnabledException : ExchangeToolsException
  {
    private readonly OofSettings enabledOof;
    private const string DateFormat = "ddd dd MMM yyyy HH:mm:ss";

    public OofAlreadyEnabledException(OofSettings enabledOof)
    {
      this.enabledOof = enabledOof;
    }

    public override string Body
    {
      get
      {
        return string.Format(@"An out of office is already set.  Details are given below.
***** This OOF has NOT been overwritten! *****

=== Duration set ===
{0} - {1}

=== Internal reply ===
{2}

=== External reply ===
{3}",
          enabledOof.Duration.StartTime.ToString(DateFormat),
          enabledOof.Duration.EndTime.ToString(DateFormat),
          enabledOof.InternalReply,
          enabledOof.ExternalReply);
      }
    }
  }
}