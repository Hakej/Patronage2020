using Patronage2020.Application.Notifications.Models;
using System.Threading.Tasks;

namespace Patronage2020.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
    }
}
