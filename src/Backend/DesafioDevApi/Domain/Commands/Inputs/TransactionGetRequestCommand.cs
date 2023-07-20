using DesafioDevApi.Domain.Common;

namespace DesafioDevApi.Domain.Commands.Inputs
{
    /// <summary>
    /// Represents a request command to retrieve a specific transaction by its Id.
    /// </summary>
    public class TransactionGetRequestCommand : Request<Response>
    {
        /// <summary>
        /// Gets the Id of the transaction to retrieve.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionGetRequestCommand"/> class with the specified transaction Id.
        /// </summary>
        /// <param name="id">The Id of the transaction to retrieve.</param>
        public TransactionGetRequestCommand(int id)
        {
            Id = id;
            Validate();
        }

        /// <summary>
        /// Validates the request command. This method checks that the Id is not null or zero,
        /// and adds any validation errors to the notifications.
        /// </summary>
        public override void Validate()
        {
            AddNotifications(new Flunt.Validations.Contract()
             .Requires()
             .IsLowerOrEqualsThan(0, Id, "400", $"O campo {nameof(Id)} não pode ser nulo ou com valor default")
             );
        }
    }
}
