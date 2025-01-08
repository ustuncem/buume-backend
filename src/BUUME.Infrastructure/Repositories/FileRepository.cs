using BUUME.Domain.Files;
using File = BUUME.Domain.Files.File;

namespace BUUME.Infrastructure.Repositories;

internal sealed class FileRepository(ApplicationDbContext dbContext) : 
    Repository<File>(dbContext), IFileRepository
{
}