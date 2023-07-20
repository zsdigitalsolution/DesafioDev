using Flunt.Notifications;
using MediatR;

namespace DesafioDevApi.Domain.Common
{
    public abstract class Request<TResponse> : Notifiable, IRequest<TResponse>
    {
        public abstract void Validate();
    }
}
