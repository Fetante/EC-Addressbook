using Addressbook.shared.Interfaces;
using System.Diagnostics;
using System;

namespace Addressbook.shared.Services;

public class FileService : IFileService
{
    public string GetContent(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
        }
        catch (Exception ex) { Debug.WriteLine("FileService - GetContent " + ex.Message); }
        return null!;
    }

    public bool SaveContentToFile(string filePath, string content)
    {
        try
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine("FileService - SaveContentToFile " + ex.Message); }
        return false;
    }
}

