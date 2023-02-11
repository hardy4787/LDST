using LDST.Application.Features.Profile.Shared.Models;
using LDST.Application.Features.Shared.Models;

namespace LDST.Application.Features.Profile.Queries.GetUserProfile;

public record UserProfileDto(string? TitlePhotoPath, string FirstName, string LastName, UserSettingsDto Settings, List<PlaygroundGeneral> playgrounds);