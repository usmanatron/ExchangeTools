using ExchangeTools.Core.Builders;
using NUnit.Framework;
using System;
using System.Linq;

namespace ExchangeTools.Core.Tests.Builders
{
  [TestFixture]
  internal class MailMessageBuilderTests
  {
    private MailMessageBuilder mailMessageBuilder;

    private const string Subject = "subject";
    private const string Body = "body";
    private const string To = "to@address.com";
    private const string From = "from@address.com";

    [SetUp]
    public void Setup()
    {
      mailMessageBuilder = new MailMessageBuilder();
    }

    [Test]
    public void BuildingWhenNothingSpecified_Throws()
    {
      Assert.Throws<ArgumentException>(() => mailMessageBuilder.Build());
    }

    [Test]
    public void MissingSubjectOnMessage_Throws()
    {
      mailMessageBuilder.WithBody(Body)
        .WithSender(From)
        .WithRecipient(To);

      Assert.Throws<ArgumentException>(() => mailMessageBuilder.Build());
    }

    [Test]
    public void MissingBodyOnMessage_Throws()
    {
      mailMessageBuilder.WithSubject(Subject)
        .WithSender(From)
        .WithRecipient(To);

      Assert.Throws<ArgumentException>(() => mailMessageBuilder.Build());
    }

    [Test]
    public void MissingSenderOnMessage_Throws()
    {
      mailMessageBuilder.WithSubject(Subject)
        .WithBody(Body)
        .WithRecipient(To);

      Assert.Throws<ArgumentException>(() => mailMessageBuilder.Build());
    }

    [Test]
    public void MissingRecipientOnMessage_Throws()
    {
      mailMessageBuilder.WithSubject(Subject)
        .WithBody(Body)
        .WithSender(From);

      Assert.Throws<ArgumentException>(() => mailMessageBuilder.Build());
    }

    [Test]
    public void EmptySenderOnMessage_Throws()
    {
      Assert.Throws<ArgumentException>(() => mailMessageBuilder.WithSender(string.Empty));
    }

    [Test]
    public void EmptyRecipientOnMessage_Throws()
    {
      Assert.Throws<ArgumentException>(() => mailMessageBuilder.WithRecipient(string.Empty));
    }

    [Test]
    public void CorrectlyBuiltMailMessage_HasAllInformationStoredAppropriately()
    {
      var message = mailMessageBuilder.WithSubject(Subject)
        .WithBody(Body)
        .WithSender(From)
        .WithRecipient(To)
        .Build();

      Assert.AreEqual(Subject, message.Subject);
      Assert.AreEqual(Body, message.Body);
      Assert.AreEqual(From, message.Sender.Address);
      Assert.AreEqual(To, message.To.Single().Address);
    }
  }
}