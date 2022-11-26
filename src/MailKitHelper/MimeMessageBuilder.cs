namespace ErgodicMage.MailKitHelper;

internal class MimeMessageBuilder
{
    private readonly EmailConfiguration _emailConfiguration;

    public MimeMessageBuilder(EmailConfiguration emailConfiguration) => _emailConfiguration = emailConfiguration;

    public MimeMessage Build(string? textBody, string? htmlBody, ICollection<string>? attachments)
    {
        var message = new MimeMessage();

        message.Subject = _emailConfiguration.Subject;

        if (_emailConfiguration.From is not null)
            message.From.Add(new MailboxAddress(_emailConfiguration.From.Name, _emailConfiguration.From.Address));

        if (_emailConfiguration.To is not null)
        {
            foreach(var to in _emailConfiguration.To)
                message.To.Add(new MailboxAddress(to.Name, to.Address));
        }

        if (_emailConfiguration.Cc is not null)
        {
            foreach(var cc in _emailConfiguration.Cc)
                message.Cc.Add(new MailboxAddress(cc.Name, cc.Address));
        }

        if (_emailConfiguration.Bcc is not null)
        {
            foreach(var bcc in _emailConfiguration.Bcc)
                message.Bcc.Add(new MailboxAddress(bcc.Name, bcc.Address));
        }

        if (_emailConfiguration.RespondTo is not null)
            message.ReplyTo.Add(new MailboxAddress(_emailConfiguration.RespondTo.Name, _emailConfiguration.RespondTo.Address));


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
