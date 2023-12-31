
namespace Addressbook.shared.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Save content to a specified file
    /// </summary>
    /// <param name="filePath">Enter filepath with extension (eg. c:\\projects\myfile.json)</param>
    /// <param name="content">Enter the content to save</param>
    /// <returns>true if success, else false</returns>
    bool SaveContentToFile(string filePath, string content);

    /// <summary>
    /// Get content from a specified file
    /// </summary>
    /// <param name="filePath">Enter the filepath with extension (eg. c:\\projects\myfile.json)</param>
    /// <returns>The content as a  string</returns>
    string GetContent(string filePath);
}
