using System.Threading.Channels;

namespace BackgroundTaskQueue.Queues
{
    public class TaskQueue : IBackgroundTaskQueue<string>
    {
        private readonly Channel<string> queue;
        public TaskQueue(IConfiguration configuration)
        {
            int.TryParse(configuration["QueueCatacity"], out int capacity);
            BoundedChannelOptions options = new(capacity)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            queue = Channel.CreateBounded<string>(options);

        }

        public async ValueTask AddQueue(string item)
        {
            ArgumentNullException.ThrowIfNull(item);

            await queue.Writer.WriteAsync(item);//aşağıda sürekli dinleme yapıyor biz ne zaman yazarsak aşağısı direk okur beklemeden
        }

        public ValueTask<string> DeQueue(CancellationToken cancellationToken)
        {
            var item = queue.Reader.ReadAsync(cancellationToken);

            return item;
        }
    }
}
