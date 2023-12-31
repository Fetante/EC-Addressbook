

using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using Addressbook.shared.Services;
using AddressBook.WPF.Interfaces;
using AddressBook.WPF.Services;
using Moq;
using Newtonsoft.Json;

namespace Addressbook.WPF.Test;

public class ContactServiceWPF_Tests
{
    /// <summary>
    /// Test to verify that the AddContact method in the ContactServiceWPF adds a single contact to the list
    /// and returns true.
    /// </summary>
    [Fact]
    public void AddContact_Should_AddOneContactToTheList_Then_ReturnTrue()
    {
        // Arrange
        IContact contact = new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        var mockFileService = new Mock<IFileService>();
        IContactServiceWPF contactService = new ContactServiceWPF(mockFileService.Object);

        //Act
        bool result = contactService.AddContact(contact);
        var contacts = contactService.GetAll();
        
        //Assert
        Assert.True(result);
        Assert.Contains(contact, contacts);
    }
    /// <summary>
    /// Test to verify that the AddContact method in the ContactServiceWPF dont add a contact to the list and return false
    /// if the contact is null.
    /// </summary>

    [Fact]
    public void AddContact_Should_ReturnFalse_If_ContactIsNull()
    {// Arrange
        IContact contact = null!;
        var mockFileService = new Mock<IFileService>();
        IContactServiceWPF contactService = new ContactServiceWPF(mockFileService.Object);

        //Act
        bool result = contactService.AddContact(contact);
        var contacts = contactService.GetAll();

        //Assert
        Assert.False(result);
        Assert.DoesNotContain(contact, contacts);
    }

    /// <summary>
    /// Test to verify that the RemoveContact method in the ContactServiceWPF removes the specified contact from the list
    /// and returns true if the contact is found, and returns false if the contact is not in the list.
    /// </summary>
    [Fact]
    public void RemoveContact_Should_RemoveContactFromList_Then_ReturnTrue_If_ContactFound()
    {
        // Arrange
        IContactServiceWPF contactService = new ContactServiceWPF(new Mock<IFileService>().Object);

        // Create a contact to be added and removed
        IContact contact = new Contact
        {            
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            Address = "Unknown",
            PhoneNumber = "070 xxx xxxx"
        };

        // Add the contact to the list
        contactService.AddContact(contact);

        //Act
        bool result = contactService.RemoveContact(contact);
        var contacts = contactService.GetAll();

        //Assert
        Assert.True(result);
        Assert.DoesNotContain(contact, contacts);
    }


    /// <summary>
    /// Test to verify that the RemoveContact method  dont removes anything and returns false 
    /// if the contact is not found in the list.
    /// </summary>
    [Fact]
    public void RemoveContact_Should_ReturnFalse_If_ContactNotFound()
    {
        // Arrange
        IContactServiceWPF contactService = new ContactServiceWPF(new Mock<IFileService>().Object);

        // Create a contact to be added and removed
        IContact contact = new Contact
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            Address = "Unknown",
            PhoneNumber = "070 xxx xxxx"
        };

        // Add the contact to the list
        contactService.AddContact(contact);

        //Act
        bool result = contactService.RemoveContact(null!);
        var contacts = contactService.GetAll();

        //Assert
        Assert.False(result);
        Assert.Single(contacts);
    }

    /// <summary>
    /// Test to verify that the UpdateContact method in the ContactServiceWPF updates the contact in the list
    /// and returns true if the contact with the specified Id is found, and returns false if the contact is not in the list.
    /// </summary>
    [Fact]
    public void UpdateContact_Should_UpdateContactInList_Then_ReturnTrueIfContactFound()
    {
        // Arrange
        IContactServiceWPF contactService = new ContactServiceWPF(new Mock<IFileService>().Object);

        // Create a contact to be added and updated
        IContact originalContact = new Contact
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@gmail.com",
            Address = "Unknown",
            PhoneNumber = "070 xxx xxxx"
        };

        contactService.AddContact(originalContact);


        originalContact.FirstName = "Jane";

        //Act
        bool result = contactService.UpdateContact(originalContact);
        var contacts = contactService.GetAll();
        var updatedContact = contacts.FirstOrDefault();

        //Assert
        Assert.True(result);
        Assert.Equal("Jane", updatedContact!.FirstName);        
    }

    /// <summary>
    /// Test for the GetAll method in ContactServiceWPF.
    /// It verifies that GetAll returns the correct list of contacts.
    /// </summary>
    [Fact]
    public void GetAll_Should_Return_CorrectListOfContacts()
    {
        // Arrange
        IContactServiceWPF contactService = new ContactServiceWPF(new Mock<IFileService>().Object);


        var contact1 = new Contact { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" };
        var contact2 = new Contact { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@gmail.com", Address = "Known", PhoneNumber = "072 xxx xxxx" };
    

        // Act
        contactService.AddContact(contact1);
        contactService.AddContact(contact2);
        var result = contactService.GetAll();
        var firstContact = result.FirstOrDefault();
        var lastContact = result.LastOrDefault();
       

        // Assert
        Assert.Equal(firstContact, contact1);
        Assert.Equal(lastContact, contact2);
    }

    /// <summary>
    /// Test for the LoadContactsFromFile method in ContactServiceWPF.
    /// It verifies that LoadContactsFromFile returns the correct list of contacts when loading from a file.
    /// </summary>
    [Fact]
    public void LoadContactsFromFile_Should_ReturnCorrectListOfContacts()
    {
        // Arrange
        var mockFileService = new Mock<IFileService>();
        IContactServiceWPF contactService = new ContactServiceWPF(mockFileService.Object);

        var contacts = new List<IContact> { new Contact { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Address = "Unknown", PhoneNumber = "070 xxx xxxx" } };

        string json = JsonConvert.SerializeObject(contacts, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

        
        mockFileService.Setup(x => x.GetContent(It.IsAny<string>())).Returns(json);

        // Act
        var result = contactService.LoadContactsFromFile();
        var firstContact = result.FirstOrDefault();

        // Assert
        Assert.Equal("John", firstContact.FirstName);
        Assert.NotNull(firstContact);
    }

}
