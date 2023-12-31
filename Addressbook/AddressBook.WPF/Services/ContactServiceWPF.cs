
using Addressbook.shared.Interfaces;
using Addressbook.shared.Models;
using AddressBook.WPF.Interfaces;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using IContactService = AddressBook.WPF.Interfaces.IContactServiceWPF;

namespace AddressBook.WPF.Services;

public partial class ContactServiceWPF : IContactServiceWPF
{
    private List<IContact> _contactList = [];

    public Contact CurrentContact { get; set; } = null!;

    private readonly IFileService _fileService;
    private readonly string _filePath = @"C:\csharp-projects\EC\customerList.json";

    public ContactServiceWPF(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = LoadContactsFromFile();
    }

    public bool AddContact(IContact contact)
    {
        try
        {
            if (contact != null)
            {
                _contactList.Add(contact);

                string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var result = _fileService.SaveContentToFile(_filePath, json);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
       
    }


    public bool RemoveContact(IContact contact)
    {
        try
        {
            var contactItem = _contactList.FirstOrDefault(x => x.Id == contact.Id);
            if (contactItem != null)
            {
                _contactList.Remove(contact);
                string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var result = _fileService.SaveContentToFile(_filePath, json);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public bool UpdateContact(IContact contact)
    {
        try
        {
            var contactItem = _contactList.FirstOrDefault(x => x.Id == contact.Id);
            if (contactItem != null)
            {
                contactItem = contact;
                string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var result = _fileService.SaveContentToFile(_filePath, json);
                return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
        
    }

    public IEnumerable<IContact> GetAll()
    {
        try
        {
            return _contactList;
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return new List<IContact>();
    }

    public List<IContact> LoadContactsFromFile()
    {
        try
        {
            string json = _fileService.GetContent(_filePath);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<List<IContact>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - LoadContactsFromFile " + ex.Message); }
        return new List<IContact>()!;
    }
}
