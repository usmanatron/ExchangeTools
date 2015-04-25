using System.Net.Mail;

namespace ExchangeOofScheduler.Core.Builders
{
  public interface IMailMessageBuilder
  {
    IMailMessageBuilder WithSubject(string subject);

    IMailMessageBuilder WithSender(string senderAddress);

    IMailMessageBuilder WithRecipient(string recipient);

    IMailMessageBuilder WithBody(string body);

    MailMessage Build();
  }
}