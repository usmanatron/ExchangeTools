using ExchangeTools.Core.Config;
using ExchangeTools.Core.Exceptions;
using ExchangeTools.Core.Exchange;
using FakeItEasy;
using NUnit.Framework;
using System.Net.Mail;

namespace ExchangeTools.Core.Tests.Exceptions
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
      var exception = new TestExchangeToolsException();

      var body = exceptionHandler.GetExceptionMessage(exception);

      StringAssert.StartsWith(TestExchangeToolsException.ExceptionMessage, body);
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