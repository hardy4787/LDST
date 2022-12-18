using ErrorOr;
using MediatR;
using PlaygroundAggregate = LDST.Domain.EFModels.Playground;

namespace LDST.Application.Playground.Commands.CreateCommand;

// TODO: maybe make sence to return whole object according to buberdinner
public sealed record CreatePlaygroundCommand(
    Guid HostId, 
    string Name, 
    string Description, 
    string Sport,
    string Address1,
    string Address2,
    string Country,
    string City,
    string State,
    string ZipCode, 
    string? TitlePhotoPath, 
    List<string>? PhotoPaths) : IRequest<ErrorOr<int>>;