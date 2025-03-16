using FluentEmail.Core;
using WascoAppTest.Models;

namespace WascoAppTest.Service
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _iFluentEmail;
        public EmailService(IFluentEmail iFluentEmail)
        {
            _iFluentEmail = iFluentEmail ?? throw new ArgumentNullException(nameof(iFluentEmail));
        }
        public async Task Send(EmailMetadata emailMetadata)
        {
            await _iFluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                .SendAsync();
        }
        public async Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, Users model, string templateFile)
        {
            await _iFluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .UsingTemplateFromFile(templateFile, model, true)
                .SendAsync();
        }
    }
}
