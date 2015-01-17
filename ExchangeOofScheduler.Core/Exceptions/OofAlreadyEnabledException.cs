using Microsoft.Exchange.WebServices.Data;
using System;
using System.Text;

namespace ExchangeOofScheduler.Core.Exceptions
{
  public class OofAlreadyEnabledException : Exception
  {
    private readonly OofSettings enabledOof;
    private const string dateFormat = "ddd dd MMM yyyy HH:mm:ss";

    public OofAlreadyEnabledException(OofSettings enabledOof)
    {
      this.enabledOof = enabledOof;
    }

    public string Body
    {
      get
      {
        var builder = new StringBuilder();
        builder.Append("An out of office is already set.  Details are given below.<br />");
        builder.Append("***** This OOF has NOT been overwritten! *****<br />&nbsp;<br />");
        builder.AppendFormat("Duration set: {0} - {1}{2}",
          enabledOof.Duration.StartTime.ToString(dateFormat),
          enabledOof.Duration.EndTime.ToString(dateFormat),
          "<br />");
        builder.Append("Internal reply: " + enabledOof.InternalReply + "<br />");
        builder.Append("External reply: " + enabledOof.ExternalReply + "<br />");

        return builder.ToString();
      }
    }
  }
}