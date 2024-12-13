namespace BUUME.SharedKernel;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
