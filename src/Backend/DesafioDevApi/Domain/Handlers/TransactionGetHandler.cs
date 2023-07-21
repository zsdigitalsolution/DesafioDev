using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Common;
using DesafioDevApi.Domain.Contract;
using DesafioDevApi.Domain.Queries;
using Flunt.Notifications;
using MediatR;

namespace DesafioDevApi.Domain.Handlers
{

    public class TransactionGetHandler : IRequestHandler<TransactionGetRequestCommand, Response>
    {
        private readonly ITransactionRepository _repository;

        public TransactionGetHandler(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(TransactionGetRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var query = TransactionGetCreateQueryCondition.CreateQueryCondition(request);
            var items = await _repository.GetByIdAsync(query, orderBy: x => x.Id);
            if (items == null)
            {
                response.AddNotification(new Notification("404", Constants.ValidateHandlerMsgNotFoundData));
                return response;
            }
            var result = new TransactionResponseCommand(id: items.Id, type: items.Type, date: items.Date, value: items.Value, cPF: items.CPF, card: items.Card, time: items.Time, storeOwner: items.StoreOwner, storeName: items.StoreName);
            response.AddValue(result);
            return response;
        }
    }
}
