using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns
{
    internal class Sample04
    {
        static void Main(string[] args)
        {
            MailMessage message = new MailMessageBuilder()
                .From("sample@gmail.com")
                .To("geekbrains@gmail.com")
                .Body("My message.")
                .Build();
        }
    }

    public class MailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage = new MailMessage();


        public MailMessage Build()
        {
            if (string.IsNullOrEmpty(_mailMessage.To))
                throw new InvalidOperationException("Can't create mail message with empty To. Please, call 'To' method first.");
            return _mailMessage;
        }

        public MailMessageBuilder From(string address)
        {
            _mailMessage.From = address;
            return this;
        }

        public MailMessageBuilder To(string address)
        {
            _mailMessage.To = address;
            return this;
        }

        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }

        public MailMessageBuilder Body(string body)
        {
            _mailMessage.Body = body;
            return this;
        }

    }
}
