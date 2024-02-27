namespace NMHAssignment.Infrastructure.Messaging.Configuration
{
    public class MessageHubOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string VHost { get; set; } = "/";
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int MaxRetryCount { get; set; }
    }
}
