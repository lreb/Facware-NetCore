using System;
using System.Collections.Generic;

namespace Facware.Services.Email.Contracts
{
    public interface IMailManager
    {
        bool Send(string fromEmail, List<string> bccList, List<string> toList, string body);
    }
}
