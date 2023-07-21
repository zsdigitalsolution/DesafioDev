using DesafioDevApi.Domain.Extensions;

namespace DesafioDevApi.Domain.Commands.Outputs
{
    /// <summary>
    /// Represents a financial transaction from a CNAB file.
    /// </summary>
    public class TransactionResponseCommand
    {
        public TransactionResponseCommand(int id, int type, DateTime date, decimal value, string cPF, string card, DateTime time, string storeOwner, string storeName)
        {
            Id = id;
            Type = type;
            Date = date;
            Value = Type.ProcessValue(value);
            CPF = cPF;
            Card = card;
            Time = time;
            StoreOwner = storeOwner;
            StoreName = storeName;
        }
        /// <summary>
        /// Gets or sets the unique identifier for the transaction.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the transaction. The value should correspond to the transaction types defined in the CNAB file specification.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the date when the transaction occurred.
        /// </summary>
        public DateTime Date { get; set; }


        /// <summary>
        /// Gets or sets the value of the transaction. This value should be normalized to the actual transaction value (i.e., the value from the CNAB file divided by 100).
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the CPF of the beneficiary of the transaction.
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Gets or sets the card number used in the transaction.
        /// </summary>
        public string Card { get; set; }

        /// <summary>
        /// Gets or sets the time when the transaction occurred.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the name of the store owner.
        /// </summary>
        public string StoreOwner { get; set; }

        /// <summary>
        /// Gets or sets the name of the store where the transaction occurred.
        /// </summary>
        public string StoreName { get; set; }
    }
}
