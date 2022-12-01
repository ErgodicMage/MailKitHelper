namespace ErgodicMage.MailKitHelper;

internal class MimeMessageBuilder
{
    private readonly EmailConfiguration _emailConfiguration;

    public MimeMessageBuilder(EmailConfiguration emailConfiguration) => _emailConfiguration = emailConfiguration;

    public MimeMessage Build(string? textBody, string? htmlBody, ICollection<string>? attachments)
    {
        var message = new MimeMessage();

        message.Subject = _emailConfiguration.Subject;

        if (InternetAddressList.TryParse(_emailConfiguration.From, out var fromEmailAddressList))
            message.From.AddRange(fromEmailAddressList);

        if (InternetAddressList.TryParse(_emailConfiguration.To, out var toEmailAddressList))
            message.To.AddRange(toEmailAddressList);

        if (InternetAddressList.TryParse(_emailConfiguration.Cc, out var ccEmailAddressList))
            message.Cc.AddRange(ccEmailAddressList);

        if (InternetAddressList.TryParse(_emailConfiguration.Bcc, out var bccEmailAddressList))
            message.Bcc.AddRange(bccEmailAddressList);

        if (InternetAddressList.TryParse(_emailConfiguration.RespondTo, out var replytoEmailAddressList))
            message.ReplyTo.AddRange(replytoEmailAddressList);

        if (Enum.TryParse<MessageImportance>(_emailConfiguration.Importance, out var importance))
            message.Importance = importance;

        if (Enum.TryParse<MessagePriority>(_emailConfiguration.Priority, out var priority))
            message.Priority = priority;

        var bodyBuilder = new BodyBuilder();

        if (string.IsNullOrWhiteSpace(textBody))
            bodyBuilder.TextBody = textBody;

        if (string.IsNullOrWhiteSpace(htmlBody))
            bodyBuilder.HtmlBody = htmlBody;

        if (attachments is not null)
        {
            foreach (string attachment in attachments)
                bodyBuilder.Attachments.Add(attachment);
        }

        message.Body = bodyBuilder.ToMessageBody();

        return message;
    }
}
