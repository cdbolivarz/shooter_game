using Godot;
using System.Threading;
using System.Threading.Tasks;

namespace Utils
{
    public static class GodotAwaiterExtensions
    {
        /// <summary>
        /// Converts a Godot SignalAwaiter into a Task that can be canceled.
        /// </summary>
        public static Task AsTask(this SignalAwaiter awaiter, CancellationToken token)
        {
            var tcs = new TaskCompletionSource();

            // When signal fires, complete the Task
            awaiter.OnCompleted(() => tcs.TrySetResult());

            // If cancellation is requested first, cancel the Task
            if (token.CanBeCanceled)
            {
                token.Register(() => tcs.TrySetCanceled());
            }

            return tcs.Task;
        }
    }
}