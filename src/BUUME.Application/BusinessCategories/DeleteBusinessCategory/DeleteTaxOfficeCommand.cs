using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.BusinessCategories.DeleteBusinessCategory;

public record DeleteBusinessCategoryCommand(Guid Id) : ICommand<bool>;