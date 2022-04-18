using System;


namespace WebService.Services.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) : base(message) // repassando a mensagem paraa classe base //
        {

        }
    }
}
