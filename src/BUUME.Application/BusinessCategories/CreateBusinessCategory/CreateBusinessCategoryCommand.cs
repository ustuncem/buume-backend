using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.BusinessCategories.CreateBusinessCategory;

public record CreateBusinessCategoryCommand(string Name) : ICommand<Guid>;