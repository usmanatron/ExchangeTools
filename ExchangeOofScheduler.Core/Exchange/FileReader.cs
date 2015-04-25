using System;
using System.IO;

namespace ExchangeOofScheduler.Core.Exchange
{
  public class FileReader : IFileReader
  {
    public string GetFileContents(string file)
    {
      var filePath = file;

      if (!Path.IsPathRooted(filePath))
      {
        filePath = Path.Combine(Environment.CurrentDirectory, filePath);
      }
      return File.ReadAllText(filePath);
    }
  }
}