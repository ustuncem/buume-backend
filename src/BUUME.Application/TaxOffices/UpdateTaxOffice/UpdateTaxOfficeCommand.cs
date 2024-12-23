using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.TaxOffices.UpdateTaxOffice;

public record UpdateTaxOfficeCommand(Guid Id, string Name) : ICommand<Guid>;