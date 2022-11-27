using MailKit.Security;

namespace ErgodicMage.MailKitHelper;

public partial class Email : IEmail
{
    #region Constructors
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly EmailConfiguration _emailConfiguration;

    public Email(SmtpConfiguration smtpConfiguration)
    {
        _smtpConfiguration = smtpConfiguration;
        _emailConfiguration = new EmailConfiguration();
    }

    public Email(SmtpConfiguration smtpConfiguration, EmailConfiguration emailConfiguration)
    {
        _smtpConfiguration = smtpConfiguration;
        _emailConfiguration = emailConfiguration;
    }
    #endregion

    #region Base Send
    public SmtpResponse Send(MimeMessage message, CancellationToken cancelationToken = default)
    {
        try
        {
            using var smtpClient = new SmtpClient();

            smtpClient.Connect(_smtpConfiguration.Host, _smtpConfiguration.Port, SecureSocketOptions.Auto, cancelationToken);

            if (_smtpConfiguration.User is not null && _smtpConfiguration.Password is not null)
                smtpClient.Authenticate(_smtpConfiguration.User, _smtpConfiguration.Password, cancelationToken);

            smtpClient.Send(message, cancelationToken);

            smtpClient.Disconnect(true, cancelationToken);
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
    public SmtpResponse Send(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
        => Send(_emailConfiguration!, body, isHtml, attachments, cancelationToken);

    public SmtpResponse Send(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
    {
        ArgumentNullException.ThrowIfNull(emailConfig);

        var messageBuilder = new MimeMessageBuilder(emailConfig);
        MimeMessage message;
        if (isHtml)
            message = messageBuilder.Build(null, body, attachments);
        else
            message = messageBuilder.Build(body, null, attachments);

        return Send(message, cancelationToken);
    }

    public SmtpResponse Send(string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
        => Send(_emailConfiguration!, textBody, htmlBody, attachments, cancelationToken);

    public SmtpResponse Send(EmailConfiguration emailConfig, string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default)
    {
        ArgumentNullException.ThrowIfNull(emailConfig);

        var messageBuilder = new MimeMessageBuilder(emailConfig);
        MimeMessage message;
        message = messageBuilder.Build(textBody, htmlBody, attachments);

        return Send(message, cancelationToken);
    }
    #endregion
}
