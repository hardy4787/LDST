using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Playground.Commands.UploadTitleImage;

public sealed class UploadTitleImageCommand : ICommand<Unit>
{
    public string PlaygroundId { get; set; } = null!;

    public IFormFile TitleImage { get; set; } = null!;

    public sealed class Handler : ICommandHandler<UploadTitleImageCommand, Unit>
    {
        private readonly IFileManager _fileManager;
        private readonly IAppDbContext _context;

        public Handler(IFileManager fileManager, IAppDbContext context)
        {
            _fileManager = fileManager;
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(UploadTitleImageCommand request, CancellationToken cancellationToken)
        {
            var playground = await _context.Playgrounds.Where(p => p.Id == int.Parse(request.PlaygroundId)).SingleOrDefaultAsync(cancellationToken);

            if (playground is null)
            {
                return DomainErrors.Playground.NotFoundPlayground;
            }
            string containerPrefix = $"playground-{playground.Id}";
            var file = request.TitleImage.ToFileInfo();
            var filePath = await _fileManager.UploadFileAsync(file, containerPrefix);

            playground.TitlePhotoPath = filePath;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
