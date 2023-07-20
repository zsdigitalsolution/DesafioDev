using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Common;
using DesafioDevApi.Domain.Contract;
using Flunt.Notifications;
using MediatR;

namespace DesafioDevApi.Domain.Handlers
{
    public class TransactionFileHandler : IRequestHandler<TransactionFileRequestCommand, Response>
    {
        private readonly ITransactionRepository _repository;
        private readonly ITransactionService _services;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionFileHandler(ITransactionRepository repository, ITransactionService services, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _services = services;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(TransactionFileRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new Response();
            var items = await _services.ParseCNABFileAsync(file: request.File);
            if (items == null)
            {
                response.AddNotification(new Notification("404", Constants.ValidateHandlerMsgNotFoundData));
                return response;
            }
            await _repository.AddAsync(items);
            if (!_unitOfWork.Commit())
            {
                response.AddNotification(new(property: "400", message: string.Format(Constants.MsgErrorCommitDataBase)));
                return response;
            }
            return response;
        }
    }
}
