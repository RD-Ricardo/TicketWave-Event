namespace CrossCutting.Abstractions
{
    public interface IUseCase<TInput, TOutput>
    {
        Task<Result<TOutput>> Execute(TInput input);
    }

    public interface IUseCase<TOutput>
    {
        Task<Result<TOutput>> Execute();
    }
}
