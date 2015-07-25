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
}