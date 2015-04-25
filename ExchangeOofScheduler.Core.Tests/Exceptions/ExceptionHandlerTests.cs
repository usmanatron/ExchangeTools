using ExchangeOofScheduler.Core.Builders;
using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Exceptions;
using ExchangeOofScheduler.Core.Exchange;
using FakeItEasy;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;
using System;
using System.Net.Mail;

namespace ExchangeOofScheduler.Core.Tests.Exceptions
{
  [TestFixture]
  internal class ExceptionHandlerTests
  {
    private IExchangeClient exchangeClient;
    private IApplicationSettings settings;
    private IMailMessageBuilder mailMessageBuilder;
    private ExceptionHandler exceptionHandler;

    [SetUp]
    public void Setup()
    {
      exchangeClient = A.Fake<IExchangeClient>();
      settings = A.Fake<IApplicationSettings>();
      mailMessageBuilder = A.Fake<IMailMessageBuilder>();

      A.CallTo(() => settings.UserEmail).Returns(@"user@mail.com");
      A.CallTo(mailMessageBuilder).WithReturnType<IMailMessageBuilder>().Returns(mailMessageBuilder);
      exceptionHandler = new ExceptionHandler(exchangeClient, settings, mailMessageBuilder);
    }

    [Test]
    public void OofAlreadyEnabledException_RendersSpecialMessageFromException()
    {
      const string expectedExceptionPrefix = @"An out of office is already set";
      var exampleOofSettings = new OofSettings
                               {
                                 InternalReply = "internal",
                                 ExternalReply = "external",
                                 Duration = new TimeWindow(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1))
                               };

      var body = exceptionHandler.GetExceptionMessage(new OofAlreadyEnabledException(exampleOofSettings));

      StringAssert.StartsWith(expectedExceptionPrefix, body);
    }

    [Test]
    public void EmailException_ReplacesNewlinesWithBrTags()
    {
      const string multiLineBody = @"Multiple
Lines";

      exceptionHandler.EmailException(multiLineBody);

      A.CallTo(() => mailMessageBuilder.WithBody("Multiple<br />Lines")).MustHaveHappened();
    }

    [Test]
    public void EmailException_SendsEmailViaExchangeClient()
    {
      const string body = @"body";

      exceptionHandler.EmailException(body);

      A.CallTo(() => exchangeClient.SendEmailToSelf(A<MailMessage>.Ignored)).MustHaveHappened();
    }
  }
}