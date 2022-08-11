using BackgroundTaskQueue.Queues;

namespace BackgroundTaskQueue
{
    public class QueueHostedService : BackgroundService
    {
        private readonly ILogger<QueueHostedService> logger;
        private readonly IBackgroundTaskQueue<string > queue;

        public QueueHostedService(IBackgroundTaskQueue<string> queue, ILogger<QueueHostedService> logger)
        {
            this.queue = queue;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var name = await queue.DeQueue(stoppingToken);//sürekli beklemede Read ile okuduğu için. biri bişi yazarsa direk okuyup işlem yapacak

                await Task.Delay(1000);//db işlemini simüle etmek için 1 saniye beklettik
                logger.LogInformation($"ExecuteAsync worked for {name}");
            }
        }
    }
}
