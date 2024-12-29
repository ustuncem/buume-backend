using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.Districts.DeleteDistrict;

public record DeleteDistrictCommand(Guid Id) : ICommand<bool>;