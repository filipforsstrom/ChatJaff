namespace ChatJaffApp.Server.Data.Contracts
{
    public interface IMessage
    {
        Guid Id { get; set; }
        Guid UserId { get; set; }
        string? Content { get; set; }
        DateTime Sent { get; }
        Guid ChatId { get; set; }
    }
}
