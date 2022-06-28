namespace Iot.Main.Domain.Shared.Units;

public interface ICommand<TRequest, TResponse>
{
    public Task<TResponse> Execute(TRequest request);
}

public interface ICommand<TResponse>
{
    public Task<TResponse> Execute();
}
