namespace GerenciadorDeLivro.Application.Models.Results;

public class ResultViewModel
{
    protected ResultViewModel(bool isSuccess , string message= "")
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public  bool IsSuccess {get;private set;}
    public string Message {get;private set;}

    public static ResultViewModel Success()
        => new(true);
    
    public static ResultViewModel Error(string message)
    => new(false,message);


}

public class ResultViewModel<T> : ResultViewModel
{
    private ResultViewModel(T? data, bool isSuccess, string message = "") : base(isSuccess, message)
    {
        Data = data;
    }

    public  T? Data {get;private set;}

    public static ResultViewModel<T> Success(T data)
        => new(data, true);
    
    public static ResultViewModel<T> Error(string message)
    => new (default,false, message);
}