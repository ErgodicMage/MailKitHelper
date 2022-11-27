namespace ErgodicMage.MailKitHelper;

public interface IEmail
{
    SmtpResponse Send(MimeMessage message, CancellationToken cancelationToken = default);
    SmtpResponse Send(string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(EmailConfiguration emailConfig, string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    SmtpResponse Send(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);

    Task<SmtpResponse> SendAsync(MimeMessage message, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string? textBody = null, string? htmlBody = null, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
    Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
}