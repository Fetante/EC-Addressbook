

namespace Addressbook.shared.Interfaces;

/// <summary>
/// Represents the basic properties of a contact.
/// </summary>
public interface IContact
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Address { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    Guid Id { get; set; }
}
