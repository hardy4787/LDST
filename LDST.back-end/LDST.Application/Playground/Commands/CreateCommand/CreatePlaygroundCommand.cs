using ErrorOr;
using MediatR;

namespace LDST.Application.Playground.Commands.CreateCommand;

// TODO: maybe make sence to return whole object according to buberdinner
public sealed record CreatePlaygroundCommand(
    Guid HostId, 
    string Name, 
    string Description, 
    int SportId,
    string Address1,
    string Address2,
    int CityId,
    string State,
    string ZipCode, 
    string? TitlePhotoPath, 
    List<string>? PhotoPaths) : IRequest<ErrorOr<int>>;