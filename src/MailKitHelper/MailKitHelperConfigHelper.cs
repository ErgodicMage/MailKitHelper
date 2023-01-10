using Microsoft.Extensions.Configuration;

namespace ErgodicMage.MailKitHelper;

public static class MailKitHelperConfigHelper
{
    public static SmtpConfiguration? GetSmtpConfiguration(IConfiguration? configuration)
        => configuration?.GetSection(nameof(SmtpConfiguration))?.Get<SmtpConfiguration>();

    public static EmailConfiguration? GetEmailConfiguration(IConfiguration? configuration)
        => configuration?.GetSection(nameof(EmailConfiguration))?.Get<EmailConfiguration>();
}
