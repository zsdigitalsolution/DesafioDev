using Flunt.Notifications;

namespace DesafioDevApi.Domain.Common
{
    /// <summary>
    /// Cria uma instãncia de <see cref="Response"/> que pode ser usado para retornar mensagens ou objetos para a Api
    /// <remarks>Nunca retorne diretamente uma instância de <see cref="Response"/>.
    /// Use as propriedades <see cref="Messages"/> ou <see cref="Value"/> na Api em conjunto com códigos HTTP que fazem sentido.</remarks>
    /// </summary>
    public class Response
    {
        private IList<Notification> MessagesList { get; } = new List<Notification>();
        /// <summary>
        /// Lista de erros do objeto
        /// </summary>
        public IReadOnlyCollection<Notification> Messages => MessagesList.ToList();
        /// <summary>
        /// Identifica se o objeto possui erros
        /// </summary>
        public bool HasMessages => MessagesList.Any();
        /// <summary>
        /// Valor de retorno
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Cria um novo objeto de retorno para a api
        /// </summary>
        public Response()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public Response(object @object) : this()
        {
            AddValue(@object);
        }

        /// <summary>
        /// Adiciona um objeto que deverá ser serializado e retornado pela Api
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public void AddValue(object @object)
        {
            Value = @object;
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddNotification(Notification notification)
        {
            MessagesList.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagens de retorno
        /// </summary>
        /// <param name="notifications">Notificações</param>
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                AddNotification(notification);
            }
        }
        /// <summary>
        /// Concatena as mensagens de erro
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(" - ", Messages.Select(x => x.Message));
        }
    }
}
