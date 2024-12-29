using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Districts.UpdateDistrict;

public record UpdateDistrictCommand(Guid Id, string Name, string Code, Guid CityId) : ICommand<Guid>;