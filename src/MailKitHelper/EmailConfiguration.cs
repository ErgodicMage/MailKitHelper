namespace ErgodicMage.MailKitHelper;

public class EmailConfiguration
{
    public string? Subject { get; set; }
    public EmailName? From { get; set; }
    public List<EmailName>? To { get; set; }
    public List<EmailName>? Cc { get; set; }
    public List<EmailName>? Bcc { get; set; }
    public EmailName? RespondTo { get; set; }
}
