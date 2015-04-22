namespace ExchangeOofScheduler.Core.Config
{
  public interface IApplicationSettings
  {
    string UserEmail { get; }

    bool DebugModeEnabled { get; }

    string StartDay { get; }

    string BoundaryTime { get; }

    string EndDay { get; }

    string InternalReplyFilename { get; }

    string SendToExternalRecipients { get; }

    string ExternalReplyFilename { get; }
  }
}