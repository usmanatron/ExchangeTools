﻿using ExchangeOofScheduler.Core.Dates;
using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core
{
  public interface IApplicationSettings
  {
    string userEmail { get; }
    bool debugModeEnabled { get; }
    DayOfWeek startDay { get; }
    DayOfWeek endDay { get; }
    DateRange nextApplicableDateRangeForOof { get; }
    string internalReply { get; }
    OofExternalAudience sendToExternalRecipients { get; }
    string externalReply { get; }
  }
}