using BUUME.Domain.CampaignTypes;
using Microsoft.EntityFrameworkCore;

namespace BUUME.Infrastructure.Repositories;

internal sealed class CampaignTypeRepository(ApplicationDbContext dbContext) : 
    Repository<CampaignType>(dbContext), ICampaignTypeRepository
{

}