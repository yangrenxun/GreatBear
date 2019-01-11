using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GreatBear.MediatR
{
    public class Event : INotification
    {
    }

    public class Handle : INotificationHandler<Event>
    {
        Task INotificationHandler<Event>.Handle(Event notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class Handle2 : INotificationHandler<Event>
    {
        Task INotificationHandler<Event>.Handle(Event notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
