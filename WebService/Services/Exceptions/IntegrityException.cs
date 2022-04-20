using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Services.Exceptions
{ // classe formada para erros de integridade referencial
    public class IntegrityException : ApplicationException // herda do applicationException
    {
        public IntegrityException(string message) : base(message)  // repassa essa chamada para a superclasse
        {
        } 
    }
}
