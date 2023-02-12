using LDST.Application.Features.Sports.Queries.Shared.Models;

namespace LDST.Application.Features.Sports.Queries.GetCitySports;

public record CitySportDto(int CityId, string CityName, IEnumerable<SportDto> Sports);

