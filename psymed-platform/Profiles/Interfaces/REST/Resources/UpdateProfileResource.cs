﻿namespace psymed_platform.Profiles.Interfaces.REST.Resources;
/// <summary>
/// Resource for updating a profile.
/// </summary>
public record UpdateProfileResource(
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