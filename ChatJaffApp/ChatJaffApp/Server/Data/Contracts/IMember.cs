namespace ChatJaffApp.Server.Data.Contracts
{
    public interface IMember
    {
        Guid Id { get; set; }
        string UserName { get; set; }
    }
}
