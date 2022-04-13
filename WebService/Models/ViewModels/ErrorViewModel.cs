using System;

namespace WebService.Models.ViewModels //ErrorViewModel � s� um modelo auxiliar para povoar as telas
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}