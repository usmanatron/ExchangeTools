using ExchangeTools.Core;
using NUnit.Framework;
using System.IO;

namespace ExchangeTools.Core.Tests
{
  [TestFixture]
  internal class FileReaderTests
  {
    private FileReader fileReader;

    private const string testFilename = "TestFile.txt";

    [SetUp]
    public void Setup()
    {
      fileReader = new FileReader();
    }

    [Test]
    public void GetFileContents_WithRelativePath_ReadsFileInSameDirectory()
    {
      var fileContents = fileReader.GetFileContents(testFilename);

      Assert.AreEqual("Test File", fileContents);
    }

    /// <remarks>
    /// We can't test this directly, as it isn't necessarily repeatable in all environments.
    /// So, we pass in a full directory path and assert that this fails (which implies
    /// it's looking for the file at that full path and not locally).
    /// </remarks>
    [Test]
    public void GetFileContents_WithFullFilePath_DoesNotEditFilePath()
    {
      const string fullFilename = @"N:\" + testFilename;

      Assert.Throws<DirectoryNotFoundException>(() => fileReader.GetFileContents(fullFilename));
    }
  }
}