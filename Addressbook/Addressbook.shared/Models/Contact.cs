
using Addressbook.shared.Interfaces;

namespace Addressbook.shared.Models;

public class Contact : IContact
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FullName => $"{FirstName} {LastName}";
}
