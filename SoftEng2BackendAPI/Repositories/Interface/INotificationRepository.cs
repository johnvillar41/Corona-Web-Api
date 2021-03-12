using SoftEng2BackendAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Repositories.Interface
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationsModel>> FetchAllNotificationsAsync();
        Task InsertNewNotification(NotificationsModel newNotification);
    }
}
