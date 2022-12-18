using ErrorOr;
using LDST.Application.Common.Interfaces.Persistance;
using MediatR;
using PlaygroundAggregate = LDST.Domain.EFModels.Playground;

namespace LDST.Application.Playground.Commands.CreateCommand
{
    public sealed class CreatePlaygroundCommandHandler : IRequestHandler<CreatePlaygroundCommand, ErrorOr<int>>
    {
        private readonly IPlaygroundRepository _playgroundRepository;

        public CreatePlaygroundCommandHandler(IPlaygroundRepository playgroundRepository)
        {
            _playgroundRepository = playgroundRepository;
        }

        public async Task<ErrorOr<int>> Handle(CreatePlaygroundCommand request, CancellationToken cancellationToken)
        {
            var menu = new PlaygroundAggregate
            {
                HostId = request.HostId,
                Name= request.Name,
                Description= request.Description,
                Sport= request.Sport,
                TitlePhotoPath= request.TitlePhotoPath,
                PhotoPaths = request.PhotoPaths ?? new(),
                Address1 = request.Address1,
                Address2 = request.Address2,
                Country = request.Country,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                AverageRating = 0
            };

            var playgroundId = await _playgroundRepository.AddAsync(menu);

            return playgroundId;
        }
    }
};