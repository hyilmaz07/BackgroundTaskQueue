namespace BackgroundTaskQueue.Queues
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask AddQueue(T item);


        ValueTask<T> DeQueue(CancellationToken cancellationToken);
    }
}
