using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Services.Mailing;
using System.IO;

namespace Altairis.NemesisEvents.BL.Services.Mailing
{
    public class SendGridMailSender : IMailSender
    {
        public void Send(MailMessageDTO message)
        {
            var sb = new StringBuilder();
            sb.AppendLine("From: " + message.From);
            sb.AppendLine("To: " + string.Join("; ", message.To));
            sb.AppendLine("CC: " + string.Join("; ", message.Cc));
            sb.AppendLine("BCC: " + string.Join("; ", message.Bcc));
            sb.AppendLine("Reply-To: " + string.Join("; ", message.ReplyTo));
            sb.AppendLine("Subject: " + message.Subject);
            sb.AppendLine();
            sb.AppendLine(message.Body);
            
            File.WriteAllText("c:\\temp\\mailpickup\\" + Guid.NewGuid() + ".txt", sb.ToString());
        }
    }
}
