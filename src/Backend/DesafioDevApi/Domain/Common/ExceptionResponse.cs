namespace DesafioDevApi.Domain.Common
{
    public class ExceptionResponse
    {
        /// <summary>
        /// Status de sucesso
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// mensagem de sucesso
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Código de identificação unida do erro
        /// </summary>
        public Guid ErroId { get; set; }
        /// <summary>
        /// Descrição do erro
        /// </summary>
        public string Erro { get; set; }
    }
}
