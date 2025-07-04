﻿using psymed_platform.Profiles.Domain.Model.Aggregates;
using psymed_platform.Profiles.Interfaces.REST.Resources;

namespace psymed_platform.Profiles.Interfaces.REST.Transform;

/// <summary>
/// Assembler for transforming Profile entity to ProfileResource.
/// </summary>
public static class ProfileResourceFromEntityAssembler
{
    /// <summary>
    /// Transforms a Profile entity to a ProfileResource.
    /// </summary>
    /// <param name="entity">The entity to transform.</param>
    /// <returns>The created resource.</returns>
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new ProfileResource(
            entity.Id,
            entity.FirstName,
            entity.LastName,
            entity.EmailAddress,
            entity.PhoneNumber,
            entity.BirthDate,
            entity.Height,
            entity.Weight,
            entity.Ubication
        );
    }
}