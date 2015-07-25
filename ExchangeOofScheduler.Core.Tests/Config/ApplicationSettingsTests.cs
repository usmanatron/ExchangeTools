using ExchangeTools.Core.Config;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ExchangeTools.Core.Tests.Config
{
  [TestFixture]
  internal class ApplicationSettingsTests
  {
    private static IDictionary<string, string> config;
    private static ApplicationSettings applicationSettings;

    [SetUp]
    public void Setup()
    {
      var configuration = A.Fake<IConfiguration>();
      config = A.Fake<IDictionary<string, string>>();
      A.CallTo(() => configuration.Config).Returns(config);

      applicationSettings = new ApplicationSettings(configuration);
    }

    [Test]
    [TestCaseSource("settingsTestCases")]
    public void Settings_RetrievedFromExpectedMembers(StringSettingsTestCase testCase)
    {
      A.CallTo(testCase.FakeCall).Returns(testCase.Value);

      var actualValue = testCase.CheckFunc.Invoke();

      Assert.AreEqual(testCase.Value, actualValue);
    }

    [Test]
    [TestCase("true", true)]
    [TestCase("True", true)]
    [TestCase("false", false)]
    [TestCase("False", false)]
    public void DebugModeSetting_ReadCorrectly(string setting, bool expectedValue)
    {
      A.CallTo(() => config["DebugModeEnabled"]).Returns(setting);

      var actualValue = applicationSettings.DebugModeEnabled;

      Assert.AreEqual(expectedValue, actualValue);
    }

    [Test]
    [TestCase("")]
    [TestCase("NotABoolean!")]
    public void InvalidDebugModeSetting_Throws(string setting)
    {
      A.CallTo(() => config["DebugModeEnabled"]).Returns(setting);

      Assert.Throws<FormatException>(() => { var a = applicationSettings.DebugModeEnabled; });
    }

    private readonly object[] settingsTestCases =
      {
        new StringSettingsTestCase(() => config["UserEmail"], "user@email.com", () => applicationSettings.UserEmail),
        new StringSettingsTestCase(() => config["StartDay"], "We", () => applicationSettings.StartDay),
        new StringSettingsTestCase(() => config["EndDay"], "Fr", () => applicationSettings.EndDay),
        new StringSettingsTestCase(() => config["BoundaryTime"], "17:30:00", () => applicationSettings.BoundaryTime),
        new StringSettingsTestCase(() => config["InternalReplyFilename"], "Internal Reply", () => applicationSettings.InternalReplyFilename),
        new StringSettingsTestCase(() => config["ExternalReplyFilename"], "External Reply", () => applicationSettings.ExternalReplyFilename),
        new StringSettingsTestCase(() => config["SendToExternalRecipients"], "Known", () => applicationSettings.SendToExternalRecipients)
      };
  }
}