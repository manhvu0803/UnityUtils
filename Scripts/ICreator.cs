// Using these interfaces instead of Func<> and Action<> allow struct to be used and avoid allocations
namespace Vun.UnityUtils
{
    public interface ICreator<out T>
    {
        T Create();

        T Invoke() => Create();
    }

    public interface ICreator<in TInput, out TOutput>
    {
        TOutput Create(TInput input);

        TOutput Invoke(TInput input) => Create(input);
    }
}