using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Common;
using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Queries;
using Flunt.Notifications;
using MediatR;

namespace DesafioDevApi.Domain.Handlers
{
    public class TransactionGetAllHandler : IRequestHandler<TransactionGetAllRequestCommand, Response>
    {
        private readonly ITransactionRepository _repository;

        public TransactionGetAllHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(TransactionGetAllRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var countItens = await _repository.CountAsync();
            if (countItens <= 0)
            {
                response.AddNotification(new Notification("404", Constants.ValidateHandlerMsgNotFoundData));
                return response;
            }
            var items = await _repository.GetAllAsync();
            var result = items.Select(x => new TransactionResponseCommand().SetPropertyAutomap(x));
            response.AddValue(result);
            return response;
        }
    }
}
