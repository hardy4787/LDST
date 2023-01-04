using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Extensions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FileInfo = LDST.Application.Models.FileInfo;

namespace LDST.Application.Features.Playground.Commands.UploadGalleryImages;

public sealed class UploadGalleryImagesCommand : ICommand<Unit>
{
    public string PlaygroundId { get; set; } = null!;

    public IFormFile[] GalleryImages { get; set; } = null!;

    public sealed class Handler : ICommandHandler<UploadGalleryImagesCommand, Unit>
    {
        private readonly IFileManager _fileManager;
        private readonly IAppDbContext _context;

        public Handler(IFileManager fileManager, IAppDbContext context)
        {
            _fileManager = fileManager;
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(UploadGalleryImagesCommand request, CancellationToken cancellationToken)
        {
            var playground = await _context.Playgrounds.Where(p => p.Id == int.Parse(request.PlaygroundId)).SingleOrDefaultAsync(cancellationToken);

            if (playground is null)
            {
                return DomainErrors.Playground.NotFoundPlayground;
            }
            string containerPrefix = $"playground-{playground.Id}";
            var files = request.GalleryImages.Select(i => i.ToFileInfo());

            var filePaths = await _fileManager.UploadFilesAsync(files, containerPrefix);

            playground.PhotoPaths = filePaths.ToList();
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
