using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Files;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Application.Businesses.UpdateBusinessContent;
using BUUME.Domain.Businesses;
using BUUME.Domain.Files;
using BUUME.SharedKernel;
using Dapper;
using File = BUUME.Domain.Files.File;

namespace BUUME.Application.Businesses.CreateBusinessContent;

internal sealed class CreateBusinessContentCommandHandler(
    IBusinessRepository businessRepository, 
    IDbConnectionFactory factory,
    IFileRepository fileRepository,
    IUnitOfWork unitOfWork,
    IFileUploader fileUploader) : ICommandHandler<CreateBusinessContentCommand, string>
{
    public async Task<Result<string>> Handle(CreateBusinessContentCommand request, CancellationToken cancellationToken)
    {
        var business = await businessRepository.GetByIdAsync(request.BusinessId, cancellationToken);
        if(business == null) return Result.Failure<string>(BusinessErrors.NotFound);
        
        var newFile = await fileUploader.UploadImageFromBase64Async(request.NewBase64Content);
        
        if(!newFile.IsSuccess) return Result.Failure<string>(BusinessErrors.ContentUploadFailure);
        
        var newFileEntity = File.Create(
            newFile.Value.Size, 
            newFile.Value.Name, 
            newFile.Value.Path,
            newFile.Value.Type);
        
        fileRepository.Add(newFileEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await HandleContent(business.Id, newFileEntity.Id);
        
        return newFile.Value.Path;
    }
    
    private async Task HandleContent(Guid businessId,Guid fileId)
    {
        const string sql = """
                           INSERT INTO business_file VALUES(@businessId, @fileId);
                           """;

        using IDbConnection connection = factory.GetOpenConnection();
        using var transaction = connection.BeginTransaction();
        
        try
        {
            await connection.ExecuteAsync(sql, new{businessId, fileId}, transaction);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

}