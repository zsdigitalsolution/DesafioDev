using DesafioDevApi.Domain.Common;

namespace DesafioDevApi.Domain.Commands.Inputs
{
    /// <summary>
    /// Represents a request command to process a CNAB file.
    /// </summary>
    public class TransactionFileRequestCommand : Request<Response>
    {
        /// <summary>
        /// Gets the uploaded CNAB file.
        /// </summary>
        public IFormFile File { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionFileRequestCommand"/> class with the specified CNAB file.
        /// </summary>
        /// <param name="file">The CNAB file to process.</param>
        public TransactionFileRequestCommand(IFormFile file)
        {
            File = file;
            Validate();
        }

        /// <summary>
        /// Validates the request command. This method checks that the CNAB file is not null, 
        /// and adds any validation errors to the notifications.
        /// </summary>
        public override void Validate()
        {
            AddNotifications(new Flunt.Validations.Contract()
             .Requires()
             .IsNotNull(File, "400", $"O campo {nameof(File)} não pode ser nulo ou com valor default")
             );
        }
    }
}