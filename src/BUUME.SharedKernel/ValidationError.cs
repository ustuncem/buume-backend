﻿namespace BUUME.SharedKernel;

public sealed record ValidationError : Error
{
    public ValidationError(Error[] errors)
        : base(
            "Validation.General",
            "One or more validation errors occurred",
            ErrorType.Validation)
    {
        Errors = errors;
    }

    public ValidationError(string Description) : base(
        "Validation.General",
        Description,
        ErrorType.Validation)
    {
        Errors = [];
    }

    public Error[] Errors { get; }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => !r.IsSuccess).Select(r => r.Error).ToArray());
}
