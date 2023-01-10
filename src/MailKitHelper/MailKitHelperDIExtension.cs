using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ErgodicMage.MailKitHelper;

public static class MailKitHelperDIExtension
{
    public static IServiceCollection AddMailKitHelper(this IServiceCollection services, SmtpConfiguration smtpConfiguration)
    {
        ServiceDescriptor sd = ServiceDescriptor.Transient<IEmail>(x => new Email(smtpConfiguration));
        services.TryAdd(sd);
        return services;
    }

    public static IServiceCollection AddMailKitHelper(this IServiceCollection services, SmtpConfiguration smtpConfiguration, EmailConfiguration emailConfiguration)
    {
        ServiceDescriptor sd = ServiceDescriptor.Transient<IEmail>(x => new Email(smtpConfiguration, emailConfiguration));
        services.TryAdd(sd);
        return services;
    }

    public static IServiceCollection AddMailKitHelper(this IServiceCollection services, IConfiguration configuration)
    {
        ServiceDescriptor sd = ServiceDescriptor.Transient<IEmail>(x => new Email(configuration));
        services.TryAdd(sd);
        return services;
    }

    //public static IServiceCollection AddMailKitHelper(this IServiceCollection services, Action<SmtpConfiguration>? smtpConfigure = null, Action<EmailConfiguration>? emailConfigure = null)
    //{
    //    var smtpConfiguration = new SmtpConfiguration();
    //    smtpConfigure?.Invoke(smtpConfiguration);
    //    var emailConfiguration = new EmailConfiguration();
    //    emailConfigure?.Invoke(emailConfiguration);
    //    ServiceDescriptor sd = ServiceDescriptor.Transient<IEmail>(x => new Email(smtpConfiguration, emailConfiguration));
    //    services.TryAdd(sd);
    //    return services;
    //}
}