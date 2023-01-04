using LDST.Application.Features.Playground.Shared.Models;
using LDST.Domain.EFModels;

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
    WeekSchedule WeekSchedule,
    List<string>? PhotoPaths);