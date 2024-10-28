namespace HotelManagementProject.Service
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
