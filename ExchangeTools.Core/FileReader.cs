using System;
using System.IO;

namespace ExchangeTools.Core
{
  public interface IFileReader
  {
    string GetFileContents(string file);
  }

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