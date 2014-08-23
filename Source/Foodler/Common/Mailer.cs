using System;
using Microsoft.Phone.Tasks;

namespace Foodler.Common
{
    public class Mailer
    {
        public static void PrepareEmail(string to, string subject, string body = null)
        {
            var emailComposeTask = new EmailComposeTask { Subject = subject, To = to, Body = body};
            emailComposeTask.Show();
        }
    }
}
