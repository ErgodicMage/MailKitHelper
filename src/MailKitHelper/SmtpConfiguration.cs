namespace ErgodicMage.MailKitHelper;

public sealed class SmtpConfiguration
{
    public string? Host { get; set; }
    public int Port { get; set; } = 0;
    public string? User { get; set; }
    public string? Password { get; set; }
}
