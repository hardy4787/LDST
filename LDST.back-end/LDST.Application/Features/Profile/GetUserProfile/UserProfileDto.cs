namespace LDST.Application.Features.Profile.GetUserProfile;

public record UserProfileDto(string? TitlePhotoPath, string FirstName, string LastName, UserSettingsDto Settings);