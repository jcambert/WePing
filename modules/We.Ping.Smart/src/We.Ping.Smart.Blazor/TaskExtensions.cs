using We.Results;

namespace We.Ping.Smart.Blazor;

public static class TaskExtensions
{
    public static async Task OnAsync<T>(this Task<Result<T>> resultTask, Action<T> onSuccess, Action<Result> onFailure,Func<T, Task> then)
    {
        Result<T> result = await resultTask;
        if ((bool)result)
        {
            onSuccess(result.Value);
            await then(result.Value);
        }
        else
        {
            onFailure(result);
        }
    }
}
