using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.TaxOffices;

namespace BUUME.Application.TaxOffices.CreateTaxOffice;

public record CreateTaxOfficeCommand(string Name) : ICommand<Guid>;