using Microsoft.Extensions.Configuration;

namespace ErgodicMage.MailKitHelper;

public static class MailKitHelperConfigHelper
{
    private const string SmtpSection  = "SmtpConfiguration";
    private const string EmailSection = "EmailConfiguration";

    public static SmtpConfiguration? GetSmtpConfiguration(IConfiguration configuration)
        => configuration?.GetSection(nameof(SmtpConfiguration))?.Get<SmtpConfiguration>();

    public static EmailConfiguration? GetEmailConfiguration(IConfiguration configuration)
        => configuration?.GetSection(nameof(EmailConfiguration))?.Get<EmailConfiguration>();
}
