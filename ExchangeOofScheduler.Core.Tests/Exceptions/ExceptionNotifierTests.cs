using System;
using ExchangeOofScheduler.Core.Exceptions;
using ExchangeOofScheduler.Core.Exchange;
using FakeItEasy;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;

namespace ExchangeOofScheduler.Core.Tests.Exceptions
{
  [TestFixture]
  class ExceptionNotifierTests
  {
    private OofSettings exampleOofSettings;
    private IExchangeClient exchangeClient;
    private IApplicationSettings settings;
    private ExceptionNotifier exceptionNotifier;

    [SetUp]
    public void Setup()
    {
      exampleOofSettings = new OofSettings
                           {
                             InternalReply = "internal",
                             ExternalReply = "external",
                             Duration = new TimeWindow(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1))
                           };

      exchangeClient = A.Fake<IExchangeClient>();
      settings = A.Fake<IApplicationSettings>();
      exceptionNotifier = new ExceptionNotifier(exchangeClient, settings);
    }

    [Test]
    public void Exception_WithMultipleLines_RendersBodyWithBRTags()
    {
      const string exceptionMessage = @"Message
Lines";
      const string expectedExceptionSuffix = @"Message<br />Lines";

      exceptionNotifier.HandleException(new Exception(exceptionMessage));

      A.CallTo(() => exchangeClient.SendEmailToSelf(A<string>.Ignored, A<string>.That.EndsWith(expectedExceptionSuffix))).MustHaveHappened();
    }

    [Test]
    public void OofAlreadyEnabledException_RendersSpecialisedBodyFromException()
    {
      const string expectedExceptionPrefix = @"An out of office is already set";

      exceptionNotifier.HandleException(new OofAlreadyEnabledException(exampleOofSettings));

      A.CallTo(() => exchangeClient.SendEmailToSelf(A<string>.Ignored, A<string>.That.StartsWith(expectedExceptionPrefix))).MustHaveHappened();
    }
  }
}
