namespace psymed_platform.Profiles.Domain.Model.Commands;

public record UpdateProfileCommand(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Weight,
    string Height,
    string Phone,
    string Ubication,
    DateTime BirthDate
);
