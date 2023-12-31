using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;

namespace AddressBook.WPF.Interfaces;

/// <summary>
/// Interface for the ContactServiceWPF, providing operations on contacts.
/// </summary>
public interface IContactServiceWPF
{
    /// <summary>
    /// Gets or sets the current contact.
    /// </summary>
    Contact CurrentContact { get; set; }

    /// <summary>
    /// Adds a new contact.
    /// </summary>
    /// <param name="contact">The contact to add.</param>
    /// <returns>True if the contact is added successfully, otherwise false.</returns>
    bool AddContact(IContact contact);

    /// <summary>
    /// Removes a contact.
    /// </summary>
    /// <param name="contact">The contact to remove.</param>
    /// <returns>True if the contact is removed successfully, otherwise false.</returns>
    bool RemoveContact(IContact contact);

    /// <summary>
    /// Updates a contact.
    /// </summary>
    /// <param name="contact">The contact to update.</param>
    /// <returns>True if the contact is updated successfully, otherwise false.</returns>
    bool UpdateContact(IContact contact);

    // <summary>
    /// Gets all contacts.
    /// </summary>
    /// <returns>An enumerable collection of contacts.</returns>
    IEnumerable<IContact> GetAll();

    /// <summary>
    /// Loads all contacts from a file.
    /// </summary>
    /// <returns>A List of IContacts</returns>
    List<IContact> LoadContactsFromFile();
}