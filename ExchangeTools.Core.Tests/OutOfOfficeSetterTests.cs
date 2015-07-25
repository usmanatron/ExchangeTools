using ExchangeTools.Core.Builders;
using ExchangeTools.Core.Exceptions;
using ExchangeTools.Core.Exchange;
using FakeItEasy;
using Microsoft.Exchange.WebServices.Data;
using NUnit.Framework;

namespace ExchangeTools.Core.Tests
{
  [TestFixture]
  internal class OutOfOfficeSetterTests
  {
    private IExchangeClient exchangeClient;
    private IOofSettingsBuilder oofSettingsBuilder;
    private OutOfOfficeSetter outOfOfficeSetter;

    [SetUp]
    public void Setup()
    {
      exchangeClient = A.Fake<IExchangeClient>();
      oofSettingsBuilder = A.Fake<IOofSettingsBuilder>();
      outOfOfficeSetter = new OutOfOfficeSetter(exchangeClient, oofSettingsBuilder);
    }

    [Test]
    [TestCase(OofState.Enabled)]
    [TestCase(OofState.Scheduled)]
    public void SettingOutOfOffice_CurrentOofStateNotDisabled_ThrowsOofAlreadyEnabledException(OofState state)
    {
      var settings = new OofSettings { State = state };
      A.CallTo(() => exchangeClient.GetOofSettings()).Returns(settings);

      Assert.Throws<OofAlreadyEnabledException>(() => outOfOfficeSetter.SetOutOfOffice());
    }

    [Test]
    public void SettingOutOfOffice_CurrentOofStateDisabled_NewOofSet()
    {
      A.CallTo(() => exchangeClient.GetOofSettings()).Returns(new OofSettings { State = OofState.Disabled });

      outOfOfficeSetter.SetOutOfOffice();

      A.CallTo(() => oofSettingsBuilder.Build()).MustHaveHappened();
      A.CallTo(() => exchangeClient.SetOofSettings(A<OofSettings>.Ignored)).MustHaveHappened();
    }
  }
}