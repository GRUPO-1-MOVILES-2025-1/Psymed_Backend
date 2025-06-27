namespace psymed_platform.Profiles.Interfaces.REST.Resources;

/// <summary>
/// Resource representing a profile.
/// </summary>
public record ProfileResource(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string Phone,
    DateTime BirthDate,
    string Height,
    string Weight,
    string Ubication
);