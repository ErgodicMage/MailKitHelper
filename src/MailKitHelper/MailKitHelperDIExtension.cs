using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ErgodicMage.MailKitHelper;

public static  class MailKitHelperDIExtension
{
    public static IServiceCollection AddMailKitHelper(this IServiceCollection services, SmtpConfiguration smtpConfiguration, EmailConfiguration emailConfiguration)
    {
        ServiceDescriptor sd = ServiceDescriptor.Transient<IEmail>(x => new Email(smtpConfiguration, emailConfiguration));
        services.TryAdd(sd);
        return services;
    }
}
