namespace ExchangeOofScheduler.Core
{
  public interface IApplicationSettings
  {
    string userEmail { get; }

    bool debugModeEnabled { get; }

    string startDay { get; }

    string boundaryTime { get; }

    string endDay { get; }

    string internalReply { get; }

    string sendToExternalRecipients { get; }

    string externalReply { get; }
  }
}