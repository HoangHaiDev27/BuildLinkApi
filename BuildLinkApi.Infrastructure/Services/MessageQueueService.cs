using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using BuildLinkApi.Application.Interfaces.Services;

namespace BuildLinkApi.Infrastructure.Services
{
    public class MessageQueueService : IMessageQueueService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public MessageQueueService(IAmazonSQS sqsClient, string queueUrl)
        {
            _sqsClient = sqsClient;
            _queueUrl = queueUrl;
        }
        public async Task PublishEmailVerificationAsync(string email, string code)
        {
            if (string.IsNullOrEmpty(_queueUrl))
            {
                throw new InvalidOperationException("AWS SQS Queue URL is not configured in appsettings.json.");
            }
            var payload = new
            {
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