using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

public record CreateTaxOfficeCommand(string Name) : ICommand<Guid>;