namespace BUUME.Domain.Businesses;

public record WorkingHours
{
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }

    private WorkingHours(TimeSpan startTime, TimeSpan endTime)
    {
        StartTime = startTime;
        EndTime = endTime;
    }

    public static WorkingHours Create(TimeSpan startTime, TimeSpan endTime)
    {
        if (startTime >= endTime)
        {
            throw new ArgumentException("Start time must be earlier than end time.");
        }

        return new WorkingHours(startTime, endTime);
    }
}