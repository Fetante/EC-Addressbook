
using Addressbook.shared.Models;

namespace AddressBook.WPF.Interfaces;

/// <summary>
/// Interface for the EditViewModel, providing operations related to editing contacts.
/// </summary>
public interface IEditViewModel
{
    /// <summary>
    /// Gets or sets the contact being edited.
    /// </summary>
    Contact Contact { get; set; }

    /// <summary>
    /// Navigates to the list view.
    /// </summary>
    void NavigateToListView();

    /// <summary>
    /// Updates the contact.
    /// </summary>
    void Update();
}
