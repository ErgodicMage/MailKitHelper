namespace ErgodicMage.MailKitHelper;

public interface IEmail
{
    SmtpConfiguration? SmtpConfiguration { get; }
    EmailConfiguration? EmailConfiguration { get; }

    SmtpResponse Send(MimeMessage message, CancellationToken cancelationToken = default);
    SmtpResponse Send(string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(EmailConfiguration emailConfig, string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);

    Task<SmtpResponse> SendAsync(MimeMessage message, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
}