namespace GerenciadorDeLivro.Application.Services;

public interface IEmailServices
{
    Task<EmailSendResult> SendEmailAsync(
        string email,
        string subject,
        string message,
        CancellationToken cancellationToken = default);
}

public sealed record EmailSendResult(bool IsSuccess)
{
    public static EmailSendResult Success() => new(true);

    public static EmailSendResult Failure() => new(false);
}
