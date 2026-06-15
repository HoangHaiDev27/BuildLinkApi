using System.Threading.Tasks;

namespace BuildLinkApi.Application.Interfaces.Services
{
    public interface IMessageQueueService
    {
        Task PublishEmailVerificationAsync(string email, string code);
    }
}