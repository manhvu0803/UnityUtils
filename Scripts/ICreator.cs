// Using these interfaces instead of Func<> and Action<> allow struct to be used and avoid allocations
namespace Vun.UnityUtils
{
    public interface ICreator<out T>
    {
        T Create();
    }

    public interface ICreator<out TOutput, in TInput>
    {
        TOutput Create(TInput input);
    }
}