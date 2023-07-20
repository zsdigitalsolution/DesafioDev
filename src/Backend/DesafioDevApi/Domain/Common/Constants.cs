namespace DesafioDevApi.Domain.Common
{
    public static class Constants
    {
        #region Handler Notification Message
        //Error
        public const string ValidateHandlerMsgNotFoundData = "Dados não encontrado";
        public const string MsgErrorCommitDataBase = "Erro ao salvar dados no banco!";
        //Success        
        public const string ValidateHandlerMsgRegisterRequetSuccess = "Solicitação registrada com sucesso.";
        public const string ValidateHandlerMsgProcessSuccessfullyCompleted = "Processo finalizado com sucesso!";
        public const string ValidateHandlerMsgInsertSuccess = "Inclusão realizada com sucesso";
        public const string ValidateHandlerMsgUpdateSuccess = "Alteração realizada com sucesso";
        #endregion

        public const string ResponseErrorMerchantCreateAdiq = "Falha no cadastro. Favor verificar lista de erros";
    }
}
