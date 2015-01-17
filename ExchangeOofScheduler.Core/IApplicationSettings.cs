namespace ExchangeOofScheduler.Core
{
  public interface IApplicationSettings
  {
    string userEmail { get; }

    bool debugModeEnabled { get; }

    OofSchedule oofSchedule { get; }

    string internalReply { get; }

    string sendToExternalRecipients { get; }

    string externalReply { get; }
  }
}