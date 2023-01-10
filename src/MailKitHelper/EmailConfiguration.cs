namespace ErgodicMage.MailKitHelper;

/// <summary>
/// Configures the email to be sent.
/// Note: this does not configure the SMTP, doing so requires the SmtpConfiguration class.
/// </summary>
public sealed class EmailConfiguration
{
    /// <summary>
    /// The subject of the email to be sent
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Who is sending the email in the form ErgodicMage@gmail.com or Ergodic Mage <ErgodicMage@gmail.com>
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// The email address(es) to send the email to.
    /// Can be in the form:
    /// ErgodicMage@gmail.com
    /// Ergodic Mage <ErgodicMage@gmail.com>
    /// Multiple email addresses are allowed seperated by a ;
    /// Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// The Carbon Copy email address(es).
    /// Can be in the form:
    /// ErgodicMage@gmail.com
    /// Ergodic Mage <ErgodicMage@gmail.com>
    /// Multiple email addresses are allowed seperated by a ;
    /// Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com
    /// </summary>
    public string? Cc { get; set; }

    /// <summary>
    /// The Blind Carbon Copy email address(es)
    /// Can be in the form:
    /// ErgodicMage@gmail.com
    /// Ergodic Mage <ErgodicMage@gmail.com>
    /// Multiple email addresses are allowed seperated by a ;
    /// Ergodic Mage <ErgodicMage@gmail.com>;John Jacob <JohnJacob@gmail.com>;BobBob@gmail.com
    /// </summary>
    public string? Bcc { get; set; }

    /// <summary>
    /// The Resond To email address in the form ErgodicMage@gmail.com or Ergodic Mage <ErgodicMage@gmail.com>
    /// 
    /// </summary>
    public string? RespondTo { get; set; }

    /// <summary>
    /// The importance of the email. Allowed values are Low, Normal and High.
    /// </summary>
    public string? Importance { get; set; }

    /// <summary>
    /// The priority of the email. Allowed values are NonUrgent, Normal and Urgent.
    /// </summary>
    public string? Priority { get; set; }
}
