using GerenciadorDeLivro.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Resend;

namespace GerenciadorDeLivro.Infrastructure.Notifiication;

public class EmailServices : IEmailServices
{
    private readonly IResend _resend;
    private readonly string _fromEmail;
    private readonly ILogger<EmailServices> _logger;

    public EmailServices(IResend resend, IConfiguration configuration, ILogger<EmailServices> logger)
    {
        _resend = resend;
        _fromEmail = configuration.GetValue<string>("Resend:FromEmail")
            ?? throw new InvalidOperationException("Resend:FromEmail não configurado.");
        _logger = logger;
    }

    public async Task<EmailSendResult> SendEmailAsync(
        string email,
        string subject,
        string message,
        CancellationToken cancellationToken = default)
    {
        var resendMessage = new EmailMessage();
        resendMessage.From = _fromEmail;
        resendMessage.To.Add(email);
        resendMessage.Subject = subject;
        resendMessage.TextBody = message;

        try
        {
            var response = await _resend.EmailSendAsync(resendMessage, cancellationToken);

            if (response.Success)
            {
                return EmailSendResult.Success();
            }

            _logger.LogWarning("O Resend recusou o envio do e-mail de recuperação de senha.");
            return EmailSendResult.Failure();
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Não foi possível enviar o e-mail de recuperação de senha pelo Resend.");
            return EmailSendResult.Failure();
        }
    }
}
