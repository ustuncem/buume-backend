using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.TaxOffices.DeleteTaxOffice;

public record DeleteTaxOfficeCommand(Guid Id) : ICommand<bool>;