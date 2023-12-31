

using Addressbook.shared.Interfaces;

namespace Addressbook.Console.Interfaces;

/// <summary>
/// Interface for the service handling the main menu in the console application.
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// Displays the main menu and handles user input.
    /// </summary>
    void Show();

    /// <summary>
    /// Displays detailed information about a specific contact.
    /// </summary>
    /// <param name="contact">The contact to display details for.</param>
    void DisplayContactDetails(IContact contact);

    /// <summary>
    /// Displays a list of all contacts in the address book.
    /// </summary>
    void DisplayAllContacts();
}
