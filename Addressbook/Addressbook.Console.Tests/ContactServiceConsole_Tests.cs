

using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using Addressbook.shared.Services;
using Addressbook.Console.Services;
using Moq;
using Newtonsoft.Json;

namespace Addressbook.Console.Tests;

public class ContactServiceConsole_Tests
{
    [Fact]
    public void AddToListShould_AddOneContactToTheList_ThenReturnTrue()
    {
        // Arrange
        IContact contact = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);
        
        //Act
        bool result = contactService.AddContactToList(contact);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void AddToListShould_Not_AddOneContactToTheList_ReturnsFalse() 
    {
        // Arrange
       
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        //Act
        bool result = contactService.AddContactToList(null);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAllContacts_Should_Return_ListOfAllContacts()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();        
        var contactService = new ContactServiceConsole(mockFileService.Object);

        IContact contact1 = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        IContact contact2 = new Contact { FirstName = "Jane", LastName = "John", Email = "jane.doe@gmail.com", Address = "Known", PhoneNumber = "072 xxx xxxx" };
        contactService.AddContactToList(contact1);
        contactService.AddContactToList(contact2);

        // Act
        var result = contactService.GetAllContacts();

        // Assert
        Assert.NotNull(result);
        var first_returned_contact = result.FirstOrDefault()!;
        Assert.Equal(contact1.FirstName, first_returned_contact.FirstName);       
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetAllContacts_Should_Return_EmptyListOfContacts_IfEmpty()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        // Act
        var result = contactService.GetAllContacts();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetContact_Should_Return_A_Contact()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        IContact contact1 = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        IContact contact2 = new Contact { FirstName = "Jane", LastName = "John", Email = "jane.doe@gmail.com", Address = "Known", PhoneNumber = "072 xxx xxxx" };
        contactService.AddContactToList(contact1);
        contactService.AddContactToList(contact2);

        //Act
        var result = contactService.GetContact(contact2.Email);

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Jane", result.FirstName);
    }

    [Fact]
    public void GetContact_Should_Return_Null_For_NonExistingEmail()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        IContact contact1 = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        IContact contact2 = new Contact { FirstName = "Jane", LastName = "John", Email = "jane.doe@gmail.com", Address = "Known", PhoneNumber = "072 xxx xxxx" };
        contactService.AddContactToList(contact1);
        contactService.AddContactToList(contact2);

        //Act
        var result = contactService.GetContact("testing");

        //Assert       
        Assert.Null(result);
    }

    [Fact]
    public void RemoveContact_Should_Return_True_If_EmailExists()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        IContact contact1 = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        IContact contact2 = new Contact { FirstName = "Jane", LastName = "John", Email = "jane.doe@gmail.com", Address = "Known", PhoneNumber = "072 xxx xxxx" };
        contactService.AddContactToList(contact1);
        contactService.AddContactToList(contact2);

        //Act
        var result = contactService.RemoveContactFromList(contact2.Email);
        var numberOfContacts = contactService.GetAllContacts();

        //Assert       
        Assert.True(result);
        Assert.Single(numberOfContacts);
    }
    [Fact]
    public void RemoveContact_Should_Return_False_If_NonExistingEmail()
    {
        //Arrange
        var mockFileService = new Mock<IFileService>();
        IContactService contactService = new ContactServiceConsole(mockFileService.Object);

        IContact contact1 = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };        
        contactService.AddContactToList(contact1);
        

        //Act
        var result = contactService.RemoveContactFromList("testing");        

        //Assert       
        Assert.False(result);        
    }


    [Fact]
    public void GetContactsFromFile_Should_Return_ListOfContacts()
    {
        // Arrange
        var contacts = new List<IContact> { new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" } };
        string json = JsonConvert.SerializeObject(contacts, Formatting.None, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

        var mockFileService = new Mock<IFileService>();
        mockFileService.Setup(x => x.GetContent(It.IsAny<string>())).Returns(json);

        var contactService = new ContactServiceConsole(mockFileService.Object);

        // Act
        var result = contactService.GetContactsFromFile();

        // Assert
        Assert.NotNull(result);
        var first_returned_contact = result.FirstOrDefault()!;

        Assert.Equal("John", first_returned_contact.FirstName);
        Assert.Single(result); // Om det finns en kontakt i JSON-strängen, förväntar vi oss att få en enda kontakt i listan.
    } 

}
