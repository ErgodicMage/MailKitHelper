using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;

namespace MailkitHelperTests;

public class ConfigurationTests
{
    private readonly ITestOutputHelper _outputHelper;

    public ConfigurationTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;

        TestingUtilities.TestNamespace = "MailkitHelperTests";
        TestingUtilities.LoadAppSettings();
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void GetSmtpConfigurationsFromIConfiguration()
    {
        var smtpConfiguration = MailKitHelperConfigHelper.GetSmtpConfiguration(TestingUtilities.Configuration);

        Assert.NotNull(smtpConfiguration);
        Assert.Equal("localhost", smtpConfiguration.Host);
        Assert.Equal(0, smtpConfiguration.Port);
        Assert.Equal("Me", smtpConfiguration.User);
        Assert.Equal("Secret", smtpConfiguration.Password);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void GetEmailConfigurationFromIConfiguration()
    {
        var emailConfiguration = MailKitHelperConfigHelper.GetEmailConfiguration(TestingUtilities.Configuration);

        Assert.NotNull(emailConfiguration);
        Assert.Equal("Hello Email Test", emailConfiguration.Subject);
        Assert.Equal("ErgodicMage@gmail.com", emailConfiguration.From);
        Assert.Equal("Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com", emailConfiguration.To);
        Assert.Null(emailConfiguration.Cc);
        Assert.Null(emailConfiguration.Bcc);
        Assert.Null(emailConfiguration.RespondTo);
    }

    [Fact]
    [Trait("Category", TestCategories.UnitTest)]
    public void FailGetEmailConfigurationFromIConfig()
    {
        var json = TestingUtilities.ReadResource("TestFiles", "appsettingsNoEmailConfiguration.json");
        var memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(json));
        var configuration = new ConfigurationBuilder()
            .AddJsonStream(memoryStream)
            .Build();

        var emailConfiguration = MailKitHelperConfigHelper.GetEmailConfiguration(configuration);

        Assert.Null(emailConfiguration);
    }

}