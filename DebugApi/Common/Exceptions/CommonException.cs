namespace DebugApi.Common.Exceptions;

public abstract class CommonException : Exception
{
    public string Code { get; }

    protected CommonException(string code, string message) : base(message)
    {
        Code = code;
    }

}