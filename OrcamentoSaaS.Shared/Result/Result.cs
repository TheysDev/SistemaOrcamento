namespace OrcamentoSaaS.Shared.Result;

public class Result
{ 
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Error { get; }

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && !string.IsNullOrWhiteSpace(error) || !isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException();
        
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success()
        => new(true, string.Empty);
    
    public static Result Fail(string error)
        => new(false, error);
}