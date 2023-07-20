using DesafioDevApi.Domain.Common;

namespace DesafioDevApi.Domain.Commands.Inputs
{
    /// <summary>
    /// Represents a request command to retrieve all transactions.
    /// </summary>
    public class TransactionGetAllRequestCommand : Request<Response>
    {
        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
