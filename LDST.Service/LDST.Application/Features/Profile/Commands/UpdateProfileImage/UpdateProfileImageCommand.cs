using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Features.Profile.Commands.UpdateProfile;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace LDST.Application.Features.Profile.Commands.UpdateProfileImage;
public sealed class UpdateProfileImageCommand : ICommand<Unit>
{
    public string UserName { get; set; } = null!;
    public IFormFile? TitleImage { get; set; }

    public sealed class Handler : ICommandHandler<UpdateProfileImageCommand, Unit>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFileManager _fileManager;

        public Handler(IFileManager fileManager, UserManager<UserEntity> userManager)
        {
            _fileManager = fileManager;
            _userManager = userManager;
        }
        public async Task<ErrorOr<Unit>> Handle(UpdateProfileImageCommand request, CancellationToken cancellationToken)
        {
            if ((await _userManager.FindByNameAsync(request.UserName)) is not UserEntity user)
            {
                return DomainErrors.Authentication.InvalidCredentials;
            }

            if (request.TitleImage != null)
            {
                string containerPrefix = request.UserName;
                var file = request.TitleImage.ToFileInfo();
                var filePath = await _fileManager.UploadFileAsync(file, containerPrefix);

                user.TitlePhotoPath = filePath;
            }
            else
            {
                await _fileManager.DeleteFileAsync(user.TitlePhotoPath!);
                user.TitlePhotoPath = null;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => Error.Validation(description: e.Description)).ToArray();
            }

            return Unit.Value;
        }
    }
}
