using ExchangeOofScheduler.Core.Dates;
using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core
{
  public interface IApplicationSettings
  {
    string userEmail { get; }
    bool debugModeEnabled { get; }

    DayOfWeek startDay { get; }
    TimeSpan startTime { get; }
    DayOfWeek endDay { get; }
    TimeSpan endTime { get; }
    
    string internalReply { get; }
    string sendToExternalRecipients { get; }
    string externalReply { get; }
  }
}