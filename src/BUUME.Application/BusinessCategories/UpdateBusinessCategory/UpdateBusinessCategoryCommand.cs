using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.BusinessCategories.UpdateBusinessCategory;

public record UpdateBusinessCategoryCommand(Guid Id, string Name) : ICommand<Guid>;