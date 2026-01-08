namespace AspProfile.Models;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public T Data { get; }

    private Result(bool success, string message, T data)
    {
        IsSuccess = success;
        Message = message;
        Data = Data;
    }

    private Result(bool success, string message)
    {
        IsSuccess = success;
        Message = message;
    }

    public static Result<T> Success(string message, T data)
        => new Result<T>(true, message, data);

    public static Result<T> Error(string message)
        => new Result<T>(false, message);
}