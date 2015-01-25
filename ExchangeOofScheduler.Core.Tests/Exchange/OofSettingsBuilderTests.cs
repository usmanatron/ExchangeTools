using ExchangeOofScheduler.Core.Config;
using ExchangeOofScheduler.Core.Dates;
using ExchangeOofScheduler.Core.Exchange;
using FakeItEasy;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;
using System;

namespace ExchangeOofScheduler.Core.Tests.Exchange
{
  [TestFixture]
  internal class OofSettingsBuilderTests
  {
    private IApplicationSettings applicationSettings;
    private IDateRangeCalculator dateRangeCalculator;
    private IOofScheduleBuilder oofScheduleBuilder;
    private OofSettingsBuilder oofSettingsBuilder;

    [SetUp]
    public void Setup()
    {
      this.applicationSettings = A.Fake<IApplicationSettings>();
      A.CallTo(() => applicationSettings.SendToExternalRecipients).Returns("All");
      this.dateRangeCalculator = A.Fake<IDateRangeCalculator>();
      this.oofScheduleBuilder = A.Fake<IOofScheduleBuilder>();

      this.oofSettingsBuilder = new OofSettingsBuilder(applicationSettings, dateRangeCalculator, oofScheduleBuilder);
    }

    [Test]
    public void InternalReplySetAppropriately()
    {
      const string internalReply = "OOF reply (internal)";
      A.CallTo(() => applicationSettings.InternalReply).Returns(internalReply);

      var settings = oofSettingsBuilder.Build();

      Assert.AreEqual(internalReply, settings.InternalReply.Message);
    }

    [Test]
    public void ExternalReplySetAppropriately()
    {
      const string externalReply = "OOF reply (external)";
      A.CallTo(() => applicationSettings.ExternalReply).Returns(externalReply);

      var settings = oofSettingsBuilder.Build();

      Assert.AreEqual(externalReply, settings.ExternalReply.Message);
    }

    [Test]
    [TestCase("all", OofExternalAudience.All)]
    [TestCase("none", OofExternalAudience.None)]
    [TestCase("known", OofExternalAudience.Known)]
    [TestCase("Known", OofExternalAudience.Known)]
    [TestCase("KnOwN", OofExternalAudience.Known)]
    public void ValidExternalAudienceSetting_ParsedCorrectlyAndStored_IrrespectiveOfCase(
      string externalAudience, OofExternalAudience expectedAudience)
    {
      A.CallTo(() => applicationSettings.SendToExternalRecipients).Returns(externalAudience);

      var settings = oofSettingsBuilder.Build();

      Assert.AreEqual(expectedAudience, settings.ExternalAudience);
    }

    [Test]
    [TestCase("invalid")]
    [TestCase("some")]
    [TestCase("")]
    public void InvalidExternalAudienceSetting_Throws(string externalAudience)
    {
      A.CallTo(() => applicationSettings.SendToExternalRecipients).Returns(externalAudience);

      Assert.Throws<ArgumentException>(() => oofSettingsBuilder.Build());
    }

    [Test]
    public void DateRange_SetAppropriately()
    {
      var range = new DateRange(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
      A.CallTo(() => dateRangeCalculator.CalculateNextDateRangeForOof(A<OofSchedule>.Ignored)).Returns(range);

      var settings = oofSettingsBuilder.Build();

      Assert.AreEqual(range.Start, settings.Duration.StartTime);
      Assert.AreEqual(range.End, settings.Duration.EndTime);
    }
  }
}