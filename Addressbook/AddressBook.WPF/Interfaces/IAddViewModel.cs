

using Addressbook.shared.Models;

namespace AddressBook.WPF.Interfaces;

/// <summary>
/// Interface for the AddViewModel, responsible for adding new contacts.
/// </summary>
public interface IAddViewModel
{
    /// <summary>
    /// Gets or sets the contact being added.
    /// </summary>
    Contact Contact { get; set; }

    /// <summary>
    /// Navigates to the list view.
    /// </summary>
    void NavigateToListView();

    /// <summary>
    /// Adds the contact to the system.
    /// </summary>
    void AddContact();
}
