using BUUME.Application.Abstractions.Messaging;

namespace BUUME.Application.TaxOffices.GetTaxOfficeById;

public sealed record GetTaxOfficeByIdQuery(Guid TaxOfficeId) : IQuery<TaxOfficeResponse>
{
}
