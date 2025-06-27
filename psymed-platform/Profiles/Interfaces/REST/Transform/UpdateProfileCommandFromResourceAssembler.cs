using psymed_platform.Profiles.Domain.Model.Commands;
using psymed_platform.Profiles.Interfaces.REST.Resources;

namespace psymed_platform.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            resource.Id,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Weight,
            resource.Height,
            resource.Phone,
            resource.Ubication,
            resource.BirthDate
        );
    }
}