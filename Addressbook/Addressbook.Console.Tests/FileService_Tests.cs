

using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using Addressbook.shared.Services;
using Moq;

namespace Addressbook.Console.Tests;

public  class FileService_Tests
{
    /// <summary>
    /// Test for the SaveContentToFile method in the FileService class.
    /// It tests for saving content with a correct filepath
    /// </summary>
    [Fact]
    public void SaveContentToFile_Should_SaveContentToFile_Then_ReturnTrue()
    {     
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"C:\csharp-projects\test.txt";
        string content = "Testing testing";

        // Act
        bool result = fileService.SaveContentToFile(filePath, content);

        //Assert
        Assert.True(result);
    }

    /// <summary>
    /// Test for the SaveContentToFile method in the FileService class.
    /// It tests for saving content with a incorrect filepath.
    /// </summary>
    [Fact]
    public void SaveContentToFile_Should_ReturnFalse_If_FilePathNotExist()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @$"C:\{Guid.NewGuid()}\test.txt";
        string content = "Testing testing";

        // Act
        bool result = fileService.SaveContentToFile(filePath, content);

        //Assert
        Assert.False(result);
    }

    /// <summary>
    /// Test for the GetContent method in the FileService class.
    /// It tests for loading content from a correct filepath.
    /// </summary>
    [Fact]
    public void GetContent_Should_ReturnContentFromFile()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @"C:\csharp-projects\test.txt";
        string content = "Testing testing";
        fileService.SaveContentToFile(filePath, content);

        // Act
        string result = fileService.GetContent(filePath);

        // Assert
        Assert.Equal(content.Trim(), result.Trim());

    }


    /// <summary>
    /// Test for the GetContent method in the FileService class.
    /// It tests for loading content from a correct filepath.
    /// </summary>
    [Fact]
    public void GetContent_Should_ReturnNull_If_FilePath_Is_NonExsisting()
    {
        // Arrange
        IFileService fileService = new FileService();
        string filePath = @$"C:\{Guid.NewGuid()}\test.txt";
        string content = "Testing testing";
        fileService.SaveContentToFile(filePath, content);

        // Act
        string result = fileService.GetContent(filePath);

        // Assert
        Assert.Null(result);
    }
}
