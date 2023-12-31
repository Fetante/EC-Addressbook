using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.WPF.Interfaces
{
    /// <summary>
    /// Interface for the ListViewModel, providing operations related to managing contacts list.
    /// </summary>
    public interface IListViewModel
    {
        /// <summary>
        /// Gets or sets the list of contacts.
        /// </summary>
        ObservableCollection<IContact> ContactList { get; set; }

        /// <summary>
        /// Navigates to the add view.
        /// </summary>
        void NavigateToAddView();

        /// <summary>
        /// Navigates to the edit view for a specific contact.
        /// </summary>
        /// <param name="contact">The contact to edit.</param>
        void NavigateToEditView(Contact contact);

        /// <summary>
        /// Removes a contact from the list.
        /// </summary>
        /// <param name="contact">The contact to remove.</param>
        void RemoveContact(Contact contact);
    }
}
