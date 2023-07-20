namespace DesafioDevApi.Domain.Common
{
    /// <summary>
    /// Resultado do processamento do handler
    /// </summary>
    /// <typeparam name="TObjeto">Objeto de retorno</typeparam>
    public class ResultMessage<TObjeto>
    {
        /// <summary>
        /// Cria uma mesagem de retorno com os dados manipulados
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public ResultMessage(bool success, string message = null, object data = null)
        {
            Success = success;
            Message = message;
            Data = (TObjeto)data;
        }
        /// <summary>
        /// Status do processamento
        /// </summary>
        public virtual bool Success { get; set; }
        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// Dados de retorno
        /// </summary>
        public virtual TObjeto Data { get; set; }

    }
}
