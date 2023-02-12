using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using MediatR;

namespace LDST.Application.Features.Sports.Commands.AddSport;

public sealed class AddSportCommand : ICommand<Unit>
{
    public string Name { get; set; } = null!;

    internal class Handler : ICommandHandler<AddSportCommand, Unit>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Unit>> Handle(AddSportCommand command, CancellationToken cancellationToken)
        {
            var sport = new SportEntity { Name = command.Name };
            _context.Sports.Add(sport);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}