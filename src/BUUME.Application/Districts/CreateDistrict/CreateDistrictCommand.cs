using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Districts.CreateDistrict;

public record CreateDistrictCommand(string Name, string Code, Guid CityId) : ICommand<Guid>;