using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ProperArch01.Domain.Services
{
    public class IdentityEmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}