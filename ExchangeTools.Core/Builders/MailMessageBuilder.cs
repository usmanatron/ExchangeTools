using System;
using System.Linq;
using System.Net.Mail;

namespace ExchangeTools.Core.Builders
{
  public interface IMailMessageBuilder
  {
    IMailMessageBuilder WithSubject(string subject);

    IMailMessageBuilder WithSender(string senderAddress);

    IMailMessageBuilder WithRecipient(string recipient);

    IMailMessageBuilder WithBody(string body);

    MailMessage Build();
  }

  public class MailMessageBuilder : IMailMessageBuilder
  {
    private readonly MailMessage mailMessage;

    public MailMessageBuilder()
    {
      mailMessage = new MailMessage();
    }

    public IMailMessageBuilder WithSubject(string subject)
    {
      mailMessage.Subject = subject;
      return this;
    }

    public IMailMessageBuilder WithSender(string senderAddress)
    {
      mailMessage.Sender = new MailAddress(senderAddress);
      return this;
    }

    public IMailMessageBuilder WithRecipient(string recipient)
    {
      mailMessage.To.Add(new MailAddress(recipient));
      return this;
    }

    public IMailMessageBuilder WithBody(string body)
    {
      mailMessage.Body = body;
      return this;
    }

    public MailMessage Build()
    {
      if (!MessageIsValid())
      {
        throw new ArgumentException("Incomplete MailMessage has been built");
      }
      return mailMessage;
    }

    private bool MessageIsValid()
    {
      return !string.IsNullOrEmpty(mailMessage.Subject) &&
             !string.IsNullOrEmpty(mailMessage.Body) &&
             mailMessage.Sender != null &&
             mailMessage.To.Any();
    }
  }
}