using System;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using BuildLinkApi.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace BuildLinkApi.Infrastructure.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public MessageQueueService(IAmazonSQS sqsClient, IConfiguration configuration)
        {
            _sqsClient = sqsClient;
            _queueUrl = configuration["AWS:SqsQueueUrl"] ?? string.Empty;
        }

        public async Task PublishEmailVerificationAsync(string email, string code)
        {
            if (string.IsNullOrEmpty(_queueUrl))
            {
                throw new InvalidOperationException("AWS SQS Queue URL is not configured in appsettings.json.");
            }

            var payload = new
            {
                JobType = "SendEmailVerification",
                Email = email,
                Code = code
            };

            var messageBody = JsonSerializer.Serialize(payload);

            var request = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = messageBody
            };

            await _sqsClient.SendMessageAsync(request);
        }
    }
}