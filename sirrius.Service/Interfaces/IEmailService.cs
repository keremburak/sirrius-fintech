using sirrius.Data.Entity;
using sirrius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(MailMessage mailMessage);
        Task SendEmailAsync(MailMessage message);
    }
}

