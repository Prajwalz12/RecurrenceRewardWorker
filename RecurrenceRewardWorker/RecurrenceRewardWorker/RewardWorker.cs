using Confluent.Kafka;
using Domain.Processors;
using Extensions;
using Newtonsoft.Json;

namespace RecurrenceRewardWorker
{
    public class RewardWorker : BackgroundService
    {
        private readonly ILogger<RewardWorker> _logger;
        private readonly IConfiguration _configuration;
        private readonly IConsumer<string, string> _messageConsumer;
        private readonly Processor _processor;

        public RewardWorker
            (
                IConfiguration configuration,
                ILogger<RewardWorker> logger, 
                ConsumerConfig consumerConfig,
                Processor processor
            )
        {
            _configuration = configuration;
            _logger = logger;
            _processor = processor;
            _messageConsumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Transaction Processing Service Started.");
            var isStopProcess = _configuration["IsStopProcess"];
            var bootStrapServers = _configuration["KafkaSettings:BootstrapServers"];
            var transactionTopic = _configuration["KafkaSettings:RecurrenceRewardTopic"];
            _logger.LogInformation($"IsStopProcess {isStopProcess} ");
            if (!String.IsNullOrEmpty(isStopProcess) && "No".Equals(isStopProcess))
            {
                string topic = transactionTopic;
                var conf = new ConsumerConfig
                {
                    GroupId = "RecurrenceReward_IIFL",
                    BootstrapServers = bootStrapServers,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    using (var builder = new ConsumerBuilder<Ignore, string>(conf).Build())
                    {
                        builder.Subscribe(topic);
                        var cancelToken = new CancellationTokenSource();
                        try
                        {
                            while (true)
                            {
                                var cr = builder.Consume(cancelToken.Token);

                                var message = cr.Message.Value;

                                _logger.LogInformation($"Receive Offset : {cr.Offset}");
                                _logger.LogInformation($"{JsonConvert.SerializeObject(new { ID = cr.Offset, Message = cr.Message, Partition = cr.TopicPartition.Partition.Value })}");
                                try
                                {
                                    await StartConsumerLoop(message).ConfigureAwait(false);
                                    await Task.Delay(1000).ConfigureAwait(false);
                                    builder.Commit(cr);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"{ex}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"{ex}");
                        }
                        finally
                        {
                            builder.Close();
                        }
                    }
                }
            }
                
        }
        private async Task StartConsumerLoop(string message)
        {
            var transactionRequest = message.GetResult<Domain.Models.RecurrenceModel.RecurrenceRewardQueueRequest>();
            await Task.Delay(0).ConfigureAwait(false); // this is just to make sure that this function runs anync.
            await _processor.ProcessAsync(new List<Domain.Models.RecurrenceModel.RecurrenceRewardQueueRequest>() { transactionRequest }).ConfigureAwait(false);
        }
        public override void Dispose()
        {
            this._messageConsumer.Close(); // Commit offsets and leave the group cleanly.
            this._messageConsumer.Dispose();

            base.Dispose();
        }

    }
}