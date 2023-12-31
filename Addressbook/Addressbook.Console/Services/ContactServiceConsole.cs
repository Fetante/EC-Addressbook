
using Addressbook.shared.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Addressbook.shared.Services;

public class ContactServiceConsole : IContactService
{
    private readonly IFileService _fileService;

    private readonly string _filePath = @"C:\csharp-projects\EC\customerList.json";
    private List<IContact> _contactList;

    public ContactServiceConsole(IFileService fileService)
    {
        _fileService = fileService;
        _contactList = GetContactsFromFile();
    }

   

    public bool AddContactToList(IContact contact)
    {
        try
        {
            if(contact != null) 
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

    public IEnumerable<IContact> GetAllContacts()
    {
        try
        {            
            return _contactList;            
        }

        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return new List<IContact>();
    }

    public IContact GetContact(string email)
    {
        try
        {
            if (!string.IsNullOrEmpty(email))
            {
                var contacts = GetAllContacts();
                var contact = contacts.FirstOrDefault(x => x.Email == email);
                return contact ??= null!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - GetContact " + ex.Message); }
        return null!;
    }

    public bool RemoveContactFromList(string email)
    {
        try
        {
            if (!string.IsNullOrEmpty(email))
            {
                var contacts = GetAllContacts();
                var contact = contacts.FirstOrDefault(x => x.Email == email);

                if (contact != null)
                {
                    _contactList.Remove(contact);
                    string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    var result = _fileService.SaveContentToFile(_filePath, json);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - RemoveContactFromList " + ex.Message); }
        return false;
    }

    public List<IContact> GetContactsFromFile()
    {
        try
        {
            string json = _fileService.GetContent(_filePath);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonConvert.DeserializeObject<List<IContact>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ContactService - GetContactsFromFile " + ex.Message); }
        return new List<IContact>()!;
    }
}
