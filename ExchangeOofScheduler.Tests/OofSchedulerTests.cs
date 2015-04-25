using ExchangeOofScheduler.Core;
using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Exceptions;
using FakeItEasy;
using NUnit.Framework;
using System;

namespace ExchangeOofScheduler.Tests
{
  [TestFixture]
  internal class OofSchedulerTests
  {
    private IOutOfOfficeSetter oofSetter;
    private IExceptionHandler exceptionHandler;
    private IApplicationSettings settings;
    private OofScheduler oofScheduler;

    [SetUp]
    public void Setup()
    {
      oofSetter = A.Fake<IOutOfOfficeSetter>();
      exceptionHandler = A.Fake<IExceptionHandler>();
      settings = A.Fake<IApplicationSettings>();
      oofScheduler = new OofScheduler(oofSetter, exceptionHandler, settings);
    }

    [Test]
    public void NoException_SilentlySucceeds()
    {
      A.CallTo(() => oofSetter.SetOutOfOffice()).DoesNothing();

      oofScheduler.ScheduleOof();

      A.CallTo(() => oofSetter.SetOutOfOffice()).MustHaveHappened();
    }

    [Test]
    public void Exception_TriggersExceptionHandler()
    {
      A.CallTo(() => oofSetter.SetOutOfOffice()).Throws<InvalidOperationException>();

      oofScheduler.ScheduleOof();

      A.CallTo(() => exceptionHandler.EmailException(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
    }

    [Test]
    public void DebugModeEnabled_WritesExceptionToConsole()
    {
      A.CallTo(() => oofSetter.SetOutOfOffice()).Throws<Exception>();
      A.CallTo(() => settings.DebugModeEnabled).Returns(true);

      oofScheduler.ScheduleOof();

      A.CallTo(() => exceptionHandler.WriteExceptionToConsole(A<string>.Ignored)).MustHaveHappened();
    }
  }
}