namespace ExchangeOofScheduler.Core
{
  public interface IApplicationSettings
  {
    string UserEmail { get; }

    bool DebugModeEnabled { get; }

    string StartDay { get; }

    string BoundaryTime { get; }

    string EndDay { get; }

    string InternalReply { get; }

    string SendToExternalRecipients { get; }

    string ExternalReply { get; }
  }
}