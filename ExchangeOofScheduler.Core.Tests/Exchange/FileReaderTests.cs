using ExchangeOofScheduler.Core.Exchange;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOofScheduler.Core.Tests.Exchange
{
  [TestFixture]
  internal class FileReaderTests
  {
    private FileReader fileReader;

    private const string testFilename = "TestFile.txt";

    [SetUp]
    public void Setup()
    {
      this.fileReader = new FileReader();
    }

    [Test]
    public void GetFileContents_WithRelativePath_ReadsFileInSameDirectory()
    {
      var fileContents = fileReader.GetFileContents(testFilename);

      Assert.AreEqual("Test File", fileContents);
    }

    [Test]
    public void GetFileContents_WithFullFilePath_DoesNotEditFilePath()
    {
    }

    /* GetFileContents_WithRelativePath_ReadsFileInSameDirectory
     * GetFileContents_WithFullFilePath_DoesNotEditFilePath
     */
  }
}