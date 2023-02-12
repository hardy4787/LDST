using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LDST.Application.Features.Playground.Commands.CreateTimeSlots;

public sealed record CreateTimeSlotsCommand : ICommand<Unit>
{
    [FromRoute(Name = "playgroundId")]
    public int PlaygroundId { get; set; }

    [FromBody]
    public List<CreateTimeSlotDto> TimeSlots { get; set; } = null!;

    internal class Handler : ICommandHandler<CreateTimeSlotsCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(CreateTimeSlotsCommand request, CancellationToken cancellationToken)
        {
            _context.GameTimeSlots.AddRange(request.TimeSlots.Select(ts => new Domain.EFModels.GameTimeSlotEntity
            {
                PlaygroundId = request.PlaygroundId,
                Price = ts.Price,
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
            }));

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
