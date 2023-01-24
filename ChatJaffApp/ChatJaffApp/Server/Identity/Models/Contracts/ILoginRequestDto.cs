namespace ChatJaffApp.Server.Identity.Models.Contracts
{
    public interface ILoginRequestDto
    {
        string? Email { get; set; }
        string? Password { get; set; }
    }
}
