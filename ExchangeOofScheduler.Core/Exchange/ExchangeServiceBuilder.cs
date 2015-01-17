using Microsoft.Exchange.WebServices.Data;
using System;

namespace ExchangeOofScheduler.Core.Exchange
{
  /// <summary>
  /// Handles setting up an Exchange Service connection.
  /// </summary>
  public class ExchangeServiceBuilder
  {
    private readonly ExchangeService exchangeService;
    private string userEmail;

    public ExchangeServiceBuilder()
    {
      exchangeService = new ExchangeService();
      SetDefaultCredentials();
    }

    /// <remarks>
    ///   This builder only supports using default credentials (i.e. the ones of the logged-in user)
    /// </remarks>
    private void SetDefaultCredentials()
    {
      exchangeService.UseDefaultCredentials = true;
    }

    public ExchangeServiceBuilder WithEmail(string email)
    {
      this.userEmail = email;
      return this;
    }

    public ExchangeServiceBuilder WithTracing(bool enableTracing)
    {
      if (enableTracing)
      {
        exchangeService.TraceEnabled = true;
        exchangeService.TraceFlags = TraceFlags.All;
      }

      return this;
    }

    public ExchangeService Build()
    {
      exchangeService.AutodiscoverUrl(userEmail, ValidateRedirectionUrl);
      return exchangeService;
    }

    /// <summary>
    /// Validate the contents of the redirection URL. In this simple validation
    /// callback, the redirection URL is considered valid if it is using HTTPS
    /// to encrypt the authentication credentials.
    /// </summary>
    private bool ValidateRedirectionUrl(string redirectionUrl)
    {
      var redirectionUri = new Uri(redirectionUrl);
      return redirectionUri.Scheme == "https";
    }
  }
}