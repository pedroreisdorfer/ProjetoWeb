using System;

namespace WebService.Models.ViewModels //ErrorViewModel � s� um modelo auxiliar para povoar as telas e foi criado automaticamente
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } // caso eu quiser criar uma mensagem customizada nesse objeto. Essa propriedade n�o foi criada automaticamente

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // a fun��o retorna se ele N�O � nulo ou vazio //
    }
}