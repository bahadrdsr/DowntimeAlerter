using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationSender
    {
        Task SendEmailAsync(string from, string to, string subject, string html);
        Task SendNotificationEmailAsync(string subject, string html);
    }
}