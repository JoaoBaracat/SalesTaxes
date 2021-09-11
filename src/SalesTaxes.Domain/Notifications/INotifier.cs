using System.Collections.Generic;

namespace SalesTaxes.Domain.Notifications
{
    public interface INotifier
    {
        bool HasNotifications();

        IList<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}