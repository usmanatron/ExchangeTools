using System.Collections.Generic;

namespace ExchangeTools.Core.Config
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

  public class ApplicationSettings : IApplicationSettings
  {
    private readonly IDictionary<string, string> applicationSettings;

    public ApplicationSettings(IConfiguration configuration)
    {
      this.applicationSettings = configuration.Config;
    }

    public string UserEmail
    {
      get
      {
        return applicationSettings["UserEmail"];
      }
    }

    public bool DebugModeEnabled
    {
      get { return bool.Parse(applicationSettings["DebugModeEnabled"]); }
    }

    public string StartDay
    {
      get { return applicationSettings["StartDay"]; }
    }

    public string BoundaryTime
    {
      get { return applicationSettings["BoundaryTime"]; }
    }

    public string EndDay
    {
      get { return applicationSettings["EndDay"]; }
    }

    public string InternalReplyFilename
    {
      get { return applicationSettings["InternalReplyFilename"]; }
    }

    public string SendToExternalRecipients
    {
      get
      {
        return applicationSettings["SendToExternalRecipients"];
      }
    }

    public string ExternalReplyFilename
    {
      get { return applicationSettings["ExternalReplyFilename"]; }
    }
  }
}