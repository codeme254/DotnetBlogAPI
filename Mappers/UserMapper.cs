using BlogAPI.DTOs;
using BlogAPI.Models;
using Riok.Mapperly.Abstractions;

namespace BlogAPI.Mappers;

// (RequiredMappingStrategy = RequiredMappingStrategy.Target) - Tells mapperly to only complain
//      when the target fields are missing mappings or not mapped
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class UserMapper
{
    [MapProperty(nameof(User.Id), nameof(GetProfileDTO.UserId))]
    [MapProperty(nameof(User.CreatedAt), nameof(GetProfileDTO.DateJoined))]
    [MapProperty(nameof(User.UpdatedAt), nameof(GetProfileDTO.LastUpdated))]
    public partial GetProfileDTO UserToGetProfileDTO(User user);
}