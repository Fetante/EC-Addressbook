using System;
using Addressbook.Console.Interfaces;
using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using Addressbook.shared.Services;

namespace Addressbook.Console.Services;

public class MenuService : IMenuService
{
    private readonly IContactService _contactService;

    public MenuService(IContactService contactService)
    {
        _contactService = contactService;
    }

    public void Show()
    {
        do
        {
            System.Console.WriteLine("****************** Welcome to the Main Menu. Here is your alternatives***********\n\n");
            System.Console.WriteLine("1. Add contact to the list");
            System.Console.WriteLine("2. Delete contact from the list");
            System.Console.WriteLine("3. View all contacts");
            System.Console.WriteLine("4. View a certain contact");
            System.Console.WriteLine("q. Quit");

             string choice = System.Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    System.Console.Clear();
                    System.Console.WriteLine("You chose option 1");
                    var contact = new Contact();
                    
                    while (string.IsNullOrWhiteSpace(contact.FirstName))
                    {
                        System.Console.WriteLine("Please enter the contacts First name");
                        contact.FirstName = System.Console.ReadLine()!;
                    }

                    while (string.IsNullOrWhiteSpace(contact.LastName))
                    {
                        System.Console.WriteLine("Please enter the contact Last name");
                        contact.LastName = System.Console.ReadLine()!;
                    }

                    
                    while (string.IsNullOrWhiteSpace(contact.PhoneNumber))
                    {
                        System.Console.WriteLine("Please enter the contacts phone number");
                        contact.PhoneNumber = System.Console.ReadLine()!;
                    }

                    
                    while (string.IsNullOrWhiteSpace(contact.Email))
                    {
                        System.Console.WriteLine("Please enter the contact email");
                        contact.Email = System.Console.ReadLine()!;
                    }

                   
                    while (string.IsNullOrWhiteSpace(contact.Address))
                    {
                        System.Console.WriteLine("Please enter the contact address");
                        contact.Address = System.Console.ReadLine()!;
                    }

                    _contactService.AddContactToList(contact);
                    System.Console.Clear();
                    break;
                case "2":
                    System.Console.Clear();
                    System.Console.WriteLine("Please enter the contact email");
                    string email = System.Console.ReadLine()!;
                    var result = _contactService.RemoveContactFromList(email);
                    if (result)
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("The contact has been removed");
                        System.Console.WriteLine("Press any key to continue...");
                        System.Console.ReadKey();
                    }
                    else
                    {
                        System.Console.Clear();
                        System.Console.WriteLine("The contact could not be removed\n");
                        System.Console.WriteLine("Press Any key to continue. . .");
                        System.Console.ReadKey();
                        System.Console.Clear();
                    }
                    break;
                case "3":
                    System.Console.Clear();
                    DisplayAllContacts();
                    break;
                case "4":
                    System.Console.Clear();                    
                    System.Console.WriteLine("Enter the contact´s Email");
                    string contactEmail = System.Console.ReadLine()!;
                    var existingContact = _contactService.GetContact(contactEmail);
                   
                    if (existingContact != null)
                    {
                        DisplayContactDetails(existingContact);
                    }
                    else
                    {
                        System.Console.Clear() ;
                        System.Console.WriteLine($"No contact found with email: {contactEmail}\n");

                        System.Console.WriteLine("Press any key to move return to the main menu . . .");
                    }
                    System.Console.ReadKey();
                    System.Console.Clear();
                    break;
                case "q":
                    return;
                default:
                    System.Console.Clear();
                    System.Console.WriteLine("Please chose a correct option");
                    break;
            }
        } while (true);
    }

    public void DisplayContactDetails(IContact contact)
    {
        System.Console.Clear();
        System.Console.WriteLine("Here are more details about your contact choice\n");
        System.Console.WriteLine($"Name: {contact.FirstName} {contact.LastName}");
        System.Console.WriteLine($"Phone: {contact.PhoneNumber}");
        System.Console.WriteLine($"Email: {contact.Email}");
        System.Console.WriteLine($"Address: {contact.Address}\n");
        System.Console.WriteLine("Press any key to continue");
    }


    public void DisplayAllContacts()
    {
        System.Console.WriteLine("********** Addressbook **********");
        System.Console.WriteLine("\n\n");
        foreach (var cont in _contactService.GetAllContacts())
        {
            string limitFirstName = cont.FirstName.Length > 20 ? cont.FirstName.Substring(0, 20) : cont.FirstName;
            string limitLastName = cont.LastName.Length > 20 ? cont.LastName.Substring(0, 20) : cont.LastName;
            System.Console.WriteLine($"{limitFirstName,-20} {limitLastName,-20} ({cont.Email})");            
        }
        System.Console.WriteLine("\n\n");
        System.Console.WriteLine("Press any key to move back to the menu");
        System.Console.ReadKey();
        System.Console.Clear();
    }

}
