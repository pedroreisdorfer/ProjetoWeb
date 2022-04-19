using System;

namespace WebService.Models.ViewModels //ErrorViewModel é só um modelo auxiliar para povoar as telas e foi criado automaticamente
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string Message { get; set; } // caso eu quiser criar uma mensagem customizada nesse objeto. Essa propriedade não foi criada automaticamente

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // a função retorna se ele NÃO é nulo ou vazio //
    }
}