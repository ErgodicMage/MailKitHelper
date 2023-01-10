using MailKit.Security;

namespace ErgodicMage.MailKitHelper;

public sealed partial class Email
{
    #region Base Send
    public async Task<SmtpResponse> SendAsync(MimeMessage message, CancellationToken cancelationToken = default)
    {
        try
        {
            using var smtpClient = new SmtpClient();

            await smtpClient.ConnectAsync(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.Auto, cancelationToken);

            if (_smtpConfiguration.User is not null && _smtpConfiguration.Password is not null)
                await smtpClient.AuthenticateAsync(_smtpConfiguration.User, _smtpConfiguration.Password, cancelationToken);

            await smtpClient.SendAsync(message, cancelationToken);

            await smtpClient.DisconnectAsync(true, cancelationToken);
        }
        catch (Exception e)
        {
            return e switch
            {
                ServiceNotConnectedException => SmtpResponse.ConnectionError,
                ServiceNotAuthenticatedException => SmtpResponse.AuthenticationError,
                InvalidOperationException => SmtpResponse.EmailNameError,
                OperationCanceledException => SmtpResponse.Canceled,
                CommandException => SmtpResponse.SendError,
                ProtocolException => SmtpResponse.ProtocolError,
                _ => SmtpResponse.OtherError
            };
        }

        return SmtpResponse.Sent;
    }
    #endregion

    #region Send Helper Functions
    public Task<SmtpResponse> SendAsync(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
        => SendAsync(_emailConfiguration!, body, isHtml, attachments, cancelationToken);

    public async Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
    {
        ArgumentNullException.ThrowIfNull(emailConfig);

        var messageBuilder = new MimeMessageBuilder(emailConfig);
        MimeMessage message;
        if (isHtml)
            message = messageBuilder.Build(null, body, attachments);
        else
            message = messageBuilder.Build(body, null, attachments);

        return await SendAsync(message, cancelationToken);
    }

    public Task<SmtpResponse> SendAsync(string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
        => SendAsync(_emailConfiguration!, textBody, htmlBody, attachments, cancelationToken);


    public async Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
    {
        ArgumentNullException.ThrowIfNull(emailConfig);

        var messageBuilder = new MimeMessageBuilder(emailConfig);
        MimeMessage message;
        message = messageBuilder.Build(textBody, htmlBody, attachments);

        return await SendAsync(message, cancelationToken);
    }
    #endregion
}
