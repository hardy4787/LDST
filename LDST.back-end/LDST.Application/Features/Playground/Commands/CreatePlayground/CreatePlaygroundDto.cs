namespace LDST.Application.Features.Playground.Commands.CreatePlayground;

public sealed record CreatePlaygroundDto(
    string Name,
    string Description,
    int SportId,
    string Address1,
    string Address2,
    int CityId,
    string State,
    string ZipCode,
    List<string>? PhotoPaths);