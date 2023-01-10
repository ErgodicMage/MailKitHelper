# MailKitHelper
MailKitHelper is a light wrapper around MailKit to provide common functionality for sending emails.

It provides simple configuration and functionality to build and send emails for standard cases.

## Basic Usage
~~~
var smtpConfig = new SmtpConfiguration()
{
    Host = "localhost"
};

var emailConfig = new EmailConfiguration()
{
    Subject = "Test Subject",
    From = "ErgodicMage@gmail.com"
    To = "Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com"
};

IEmail email = new Email(smtpConfiguration, emailConfiguration);
SmtpResponse response = _email.Send($"This is a test email sent by ErgodicMage at {DateTime.Now}");
~~~

Or the EmailConfiguration can be set with the Send method, allowing for different email subjects, to and such for multiple emails.
~~~
var smtpConfig = new SmtpConfiguration()
{
    Host = "localhost"
};

var emailConfig = new EmailConfiguration()
{
    Subject = "Test Subject",
    From = "ErgodicMage@gmail.com"
    To = "Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com"
};

IEmail email = new Email(smtpConfiguration);
SmtpResponse response = _email.Send(emailConfiguration, $"This is a test email sent by ErgodicMage at {DateTime.Now}");
~~~

## IEmail functions
The configuration options can be accessed and modified.
~~~
SmtpConfiguration? SmtpConfiguration { get; }
EmailConfiguration? EmailConfiguration { get; }
~~~

Simple functions for sending text and html emails with attachments.
~~~
SmtpResponse Send(string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
SmtpResponse Send(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
SmtpResponse Send(EmailConfiguration emailConfig, string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
SmtpResponse Send(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
~~~

Asyncronous functions are also available.
~~~   
Task<SmtpResponse> SendAsync(string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
Task<SmtpResponse> SendAsync(string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string textBody, string htmlBody, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
Task<SmtpResponse> SendAsync(EmailConfiguration emailConfig, string body, bool isHtml = false, ICollection<string>? attachments = null, CancellationToken cancelationToken = default);
~~~

More complex emails can be sent using MailKit's MimeMessage directly.
~~~
SmtpResponse Send(MimeMessage message, CancellationToken cancelationToken = default);
Task<SmtpResponse> SendAsync(MimeMessage message, CancellationToken cancelationToken = default); 
~~~

## Microsoft Dependency Injection
MailKitHelper can easily be used with .NET Dependency Injection.

### Example with IConfiguration DI
~~~
var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<IEmail, Email>()
                }
                .Build();

public class SendNotificationEmail
{
    private readonly ILogger<SendNotificationEmail> _logger;
    private readonly IEmail _email;
    public SendNotificationEmail(ILogger<SendNotificationEmail> logger, IEmail email)
    {
        _logger = logger;
        _email = email;
    }

    public async Task<bool> SendNotification(string notification, string attachFile)
    {
        _logger.LogInformation("Sending notification {0} to {1}", notification, _email.EmailConfiguration.To);
        var response = await _email.SendAsync(notification, true, new List<string>(){attachedFile});
        if (response == SmtpResponse.Sent)
            _logger.LogInformation("Email sent");
        else
            _logger.LogError("Email was not sent {0}", response);
        return return response == SmtpResponse.Sent ? true : false;
    }
}
~~~

Any recommendations, suggestions and constructive criticism is always welcome.