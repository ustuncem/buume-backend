using System.Data;
using BUUME.Application.Abstractions.Data;
using BUUME.Application.Abstractions.Files;
using BUUME.Application.Abstractions.Messaging;
using BUUME.Domain.Businesses;
using BUUME.Domain.Files;
using BUUME.SharedKernel;
using Dapper;
using File = BUUME.Domain.Files.File;

namespace BUUME.Application.Businesses.UpdateBusinessContent;

internal sealed class UpdateBusinessContentCommandHandler(
    IBusinessRepository businessRepository, 
    IDbConnectionFactory factory,
    IFileRepository fileRepository,
    IUnitOfWork unitOfWork,
    IFileUploader fileUploader) : ICommandHandler<UpdateBusinessContentCommand, bool>
{
    public async Task<Result<bool>> Handle(UpdateBusinessContentCommand request, CancellationToken cancellationToken)
    {
        var business = await businessRepository.GetByIdAsync(request.BusinessId, cancellationToken);
        if(business == null) return Result.Failure<bool>(BusinessErrors.NotFound);
        
        var oldFile = await fileRepository.GetByIdAsync(request.FileId, cancellationToken);
        if(oldFile == null) return Result.Failure<bool>(BusinessErrors.ContentNotFound);
        
        fileRepository.HardDelete(oldFile);
        var fileDeletionResult = fileUploader.DeleteFile(oldFile.Path.Value);

        if (!fileDeletionResult) return Result.Failure<bool>(BusinessErrors.ContentDeletionFailure);

        var newFile = await fileUploader.UploadImageFromBase64Async(request.NewBase64Content);
        
        if(!newFile.IsSuccess) return Result.Failure<bool>(BusinessErrors.ContentUploadFailure);
        
        var newFileEntity = File.Create(
            newFile.Value.Size, 
            newFile.Value.Name, 
            newFile.Value.Path,
            newFile.Value.Type);

        if (request.Mode == "logo")
        {
            business.UpdateLogo(newFileEntity.Id);
            businessRepository.Update(business);
        }
        
        fileRepository.Add(newFileEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (request.Mode != "logo")
        {
            await HandleNonLogoContent(business.Id, newFileEntity.Id);
        }
        
        return true;
    }

    private async Task HandleNonLogoContent(Guid businessId,Guid fileId)
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