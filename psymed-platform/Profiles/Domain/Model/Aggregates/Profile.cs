using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using psymed_platform.Profiles.Domain.Model.Commands;
using psymed_platform.Profiles.Domain.Model.ValueObjects;

namespace psymed_platform.Profiles.Domain.Model.Aggregates;

[Table("Profiles")]
public partial class Profile
{
    [Key]
    public int Id { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public string UserId { get; set; } = string.Empty;

    public PersonName Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public RoleType Role { get; private set; }
    public PhoneNumber Phone { get; private set; }

    public string Weight { get; set; }
    public string Height { get; set; }

    public string FirstName => Name.FirstName;
    public string LastName => Name.LastName;
    public string EmailAddress => Email.Address;
    public string PhoneNumber => Phone.Number;
    public string RoleType => Role.Role;
    public string Ubication { get; private set; } = string.Empty;

    public Profile()
    {
        Name = new PersonName();
        Email = new EmailAddress();
        Phone = new PhoneNumber();
        Role = new RoleType();
        Weight = string.Empty;
        Height = string.Empty;
    }

    public Profile(string firstName, string lastName, string email, string weight, string height, string phone, string role, string userId, DateTime birthDate, string ubication)
    {
        Name = new PersonName(firstName, lastName);
        Email = new EmailAddress(email);
        Phone = new PhoneNumber(phone);
        Role = new RoleType(role);
        Weight = weight;
        Height = height;
        UserId = userId;
        BirthDate = birthDate;
        Ubication = ubication;
    }

    public Profile(CreateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Phone = new PhoneNumber(command.Phone);
        Role = new RoleType(command.Role);
        Height = command.Height;
        Weight = command.Weight;
        UserId = command.UserId;
        BirthDate = command.BirthDate;
        Ubication = command.Ubication;
    }

    public void Update(UpdateProfileCommand command)
    {
        Name = new PersonName(command.FirstName, command.LastName);
        Email = new EmailAddress(command.Email);
        Phone = new PhoneNumber(command.Phone);
        Height = command.Height;
        Weight = command.Weight;
        Ubication = command.Ubication;
        BirthDate = command.BirthDate;
    }
}
