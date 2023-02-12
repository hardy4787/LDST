namespace LDST.Domain.Mail;

public class Message
{
    public IEnumerable<string> To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Content { get; set; } = string.Empty;
    public FileInfo[]? Attachments { get; set; } = null;
}
