namespace ShoppingCartIDM.Common;

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public List<string> Errors { get; init; } = [];
    public string? ErrorMessage { get; init; }

    public static Result<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    public static Result<T> Failure(string error) => new()
    {
        IsSuccess = false,
        ErrorMessage = error,
        Errors = new List<string> { error }
    };

    public static Result<T> Failure(List<string> errors) => new()
    {
        IsSuccess = false,
        Errors = errors,
        ErrorMessage = string.Join(", ", errors)
    };
}

public class Result
{
    public bool IsSuccess { get; init; }
    public List<string> Errors { get; init; } = [];
    public string? ErrorMessage { get; init; }

    public static Result Success() => new()
    {
        IsSuccess = true
    };

    public static Result Failure(string error) => new()
    {
        IsSuccess = false,
        ErrorMessage = error,
        Errors = new List<string> { error }
    };

    public static Result Failure(List<string> errors) => new()
    {
        IsSuccess = false,
        Errors = errors,
        ErrorMessage = string.Join(", ", errors)
    };
}