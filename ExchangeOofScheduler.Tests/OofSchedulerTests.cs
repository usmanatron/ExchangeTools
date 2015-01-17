using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Exceptions;
using ExchangeOofScheduler.Core.Exchange;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace ExchangeOofScheduler.Tests
{
  [TestFixture]
  internal class OofSchedulerTests
  {
    private IOutOfOfficeSetter oofSetter;
    private IExchangeClient exchangeClient;
    private OofScheduler oofScheduler;
    private IExceptionNotifier fakeExceptionNotifier;

    [SetUp]
    public void Setup()
    {
      oofSetter = A.Fake<IOutOfOfficeSetter>();
      exchangeClient = A.Fake<IExchangeClient>();
      fakeExceptionNotifier = A.Fake<IExceptionNotifier>();
    }

    [Test]
    public void NoException_SilentlySucceeds()
    {
      oofScheduler = new OofScheduler(oofSetter, fakeExceptionNotifier);
      A.CallTo(() => oofSetter.SetOutOfOffice()).DoesNothing();

      oofScheduler.ScheduleOof();

      A.CallTo(() => oofSetter.SetOutOfOffice()).MustHaveHappened();
    }

    [Test]
    public void Exception_TriggersExceptionHandler()
    {
      oofScheduler = new OofScheduler(oofSetter, fakeExceptionNotifier);
      A.CallTo(() => oofSetter.SetOutOfOffice()).Throws<InvalidOperationException>();

      oofScheduler.ScheduleOof();

      A.CallTo(() => fakeExceptionNotifier.HandleException(A<Exception>.Ignored)).WithAnyArguments().MustHaveHappened();
    }

    [Test]
    public void Exception_SendsAnEmail()
    {
      var exceptionNotifier = new ExceptionNotifier(exchangeClient, A.Fake<IApplicationSettings>());
      oofScheduler = new OofScheduler(oofSetter, exceptionNotifier);
      A.CallTo(() => oofSetter.SetOutOfOffice()).Throws<InvalidOperationException>();

      oofScheduler.ScheduleOof();

      A.CallTo(() => exchangeClient.SendEmailToSelf(A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
    }
  }
}