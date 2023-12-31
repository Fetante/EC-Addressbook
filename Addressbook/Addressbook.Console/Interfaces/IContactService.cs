
namespace Addressbook.shared.Interfaces;

/// <summary>
/// Interface for a contact service, providing operations to manage contacts.
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Adds a contact to the list.
    /// </summary>
    /// <param name="contact">The contact to add.</param>
    /// <returns>True if the contact is successfully added, otherwise false.</returns>
    bool AddContactToList(IContact contact);

    /// <summary>
    /// Removes a contact from the list based on email.
    /// </summary>
    /// <param name="email">The email of the contact to remove.</param>
    /// <returns>True if the contact is successfully removed, otherwise false.</returns>
    bool RemoveContactFromList(string email);

    /// <summary>
    /// Gets all contacts in the list.
    /// </summary>
    /// <returns>An IEnumerable of contacts.</returns>
    IEnumerable<IContact> GetAllContacts();

    /// <summary>
    /// Gets a specific contact based on email.
    /// </summary>
    /// <param name="email">The email of the contact to retrieve.</param>
    /// <returns>The contact if found, otherwise null.</returns>
    IContact GetContact(string email);

    /// <summary>
    /// Gets all contacts from a file
    /// </summary>
    /// <returns>All the contacts in the file</returns>
    List<IContact> GetContactsFromFile();
}
