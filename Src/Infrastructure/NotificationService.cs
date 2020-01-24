using Patronage2020.Application.Common.Interfaces;
using Patronage2020.Application.Notifications.Models;
using System.Threading.Tasks;

namespace Patronage2020.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
