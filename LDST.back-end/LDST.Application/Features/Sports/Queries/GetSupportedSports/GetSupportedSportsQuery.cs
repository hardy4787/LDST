using ErrorOr;
using LDST.Application.Abstractions;
using LDST.Application.Features.Sports.Queries.Shared.Models;
using LDST.Application.Interfaces.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LDST.Application.Features.Sports.Queries.GetSupportedSports;

public sealed class GetSupportedSportsQuery : IQuery<List<SportDto>>
{
    internal class Handler : IQueryHandler<GetSupportedSportsQuery, List<SportDto>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<List<SportDto>>> Handle(GetSupportedSportsQuery query, CancellationToken cancellationToken)
        {
            return await _context.Sports
                    .Select(x => new SportDto(x.Id, x.Name))
                    .ToListAsync(cancellationToken);
        }
    }
}
