﻿using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Application.Features.Playground.Commands.CreatePlayground;

public sealed class CreatePlaygroundCommand : ICommand<int>
{
    [FromRoute(Name = "hostId")]
    public string HostId { get; set; } = null!;

    [FromBody]
    public CreatePlaygroundDto Playground { get; set; } = null!;

    internal class Handler : ICommandHandler<CreatePlaygroundCommand, int>
    {
        private readonly IAppDbContext _context;

        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<int>> Handle(CreatePlaygroundCommand request, CancellationToken cancellationToken)
        {
            var playground = new PlaygroundEntity
            {
                HostId = request.HostId,
                Name = request.Playground.Name,
                Description = request.Playground.Description,
                SportId = request.Playground.SportId,
                PhotoPaths = request.Playground.PhotoPaths ?? new(),
                Address1 = request.Playground.Address1,
                Address2 = request.Playground.Address2,
                CityId = request.Playground.CityId,
                State = request.Playground.State,
                ZipCode = request.Playground.ZipCode,
                WeekSchedule = new WeekScheduleEntity
                {
                    Days = request.Playground.WeekSchedule.Days.Select(d => new DayScheduleEntity
                    {
                        IsClosed = d.IsClosed,
                        OpeningTime= d.OpeningTime,
                        ClosingTime= d.ClosingTime,
                        DayOfWeek = d.DayOfWeek
                    }).ToList(),
                },
                AverageRating = 0
            };

            _context.Playgrounds.Add(playground);

            if (!_context.CitySports.Any(x => x.CityId == request.Playground.CityId && x.SportId == request.Playground.SportId))
            {
                _context.CitySports.Add(new CitySportEntity { SportId = request.Playground.SportId, CityId = request.Playground.CityId });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return playground.Id;
        }
    }
}
