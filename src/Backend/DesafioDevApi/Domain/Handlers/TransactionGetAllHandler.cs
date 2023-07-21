using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Common;
using DesafioDevApi.Domain.Contract;
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
            var result = items.Select(x => new TransactionResponseCommand(id: x.Id, type: x.Type, date: x.Date, value: x.Value, cPF: x.CPF, card: x.Card, time: x.Time, storeOwner: x.StoreOwner, storeName: x.StoreName));
            response.AddValue(result);
            return response;
        }
    }
}
