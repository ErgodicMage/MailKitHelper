namespace ErgodicMage.MailKitHelper;

public enum SmtpResponse
{
    Sent,
    Canceled,
    ConnectionError,
    AuthenticationError,
    EmailNameError,
    SendError,
    ProtocolError,
    OtherError
}
