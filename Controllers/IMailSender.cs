using SendGrid;
using System.Runtime.Remoting;

namespace ShowcaseWebdev.Controllers
{
    public interface IMailSender
    {
        async Response Send(Imessage);
    }
}
