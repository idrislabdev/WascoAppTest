using WascoAppTest.Models;

namespace WascoAppTest.Service
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);
        Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, Users model, string templateFile);
    }
}
